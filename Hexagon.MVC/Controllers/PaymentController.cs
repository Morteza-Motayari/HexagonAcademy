using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Orders;
using Hexagon.Application.Services.Interfaces.Payment;
using Hexagon.Application.Services.Interfaces.Users;
using Hexagon.Application.Services.Interfaces.Wallets;
using Hexagon.Domain.DTOs.NovinoPay;
using Hexagon.Domain.Models.Orders;
using Hexagon.Domain.Models.Wallets;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Orders.Orders;
using Hexagon.Domain.ViewModels.Payment;
using Hexagon.Domain.ViewModels.Users.Users;
using Hexagon.Domain.ViewModels.Wallets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Controllers
{
    public class PaymentController(INovinoService novinoService
        , IWalletService walletService
        , IOrderService orderService,
        IUserService userService) : BaseSiteController

    {
        [Authorize]
        public async Task<IActionResult> StartPaybyNovino(int? walletId, int? orderId)
        {
            ClientSideWalletViewModel? wallet = null;
            ClientSideOrderViewModel? order = null;
            int price = 0;
            string invoiceId = "";

            if (walletId.HasValue)
            {
                wallet = await walletService.GetWalletClientSideAsync(walletId.Value);

                if (wallet == null)
                    return NotFound();
                price = (int)wallet.Price;
                invoiceId = $"wallet_{wallet.Id}";
            }

            if (orderId.HasValue)
            {
                order = await orderService.GetOrderClientSideAsync(orderId.Value);
                if (order == null)
                    return NotFound();
                price = order.TotalPrice;
                invoiceId = $"order_{orderId}";
            }
            UserClientSideView? user = await userService.GetUserClientSideAsync(User.GetUserId());

            #region Send request to payment gateway
            var result = await novinoService.CreateRequestAsync(new NovinoGetPaymentUrlRequestDto
            {
                MerchantId = "test",
                Amount = price,
                CallbackUrl = $"https://localhost:7275/payment/NovinoCallback?walletId={wallet?.Id}&orderId={order?.Id}",
                Description = InfoMessages.ChargeWallet,
                InvoiceId = invoiceId,
                CallbackMethod = "POST",
                Email = user.email,
                Mobile = user.PhoneNumber,
                Name = $"{user.GetUserName()}"
            });

            #endregion

            if (result.Status != "100")
            {
                TempData[ErrorMessage] = ErrorMessages.ErrorOccuredWhileEnteringGateway;
                return RedirectToAction("Index", "Home");
            }

            #region Saving Authority in Wallet
            if (wallet != null)
            {
                await walletService.ClientSideUpdateWalletAuthorityAsync(wallet.Id, result.Data.Authority);
            }
            else if (order != null)
            {
                await walletService.ClientSideUpdateWalletAuthorityThroughOrderAsync(order.Id, result.Data.Authority);
            }
            #endregion

            return Redirect(result.Data.PaymentUrl);
        }
        [HttpPost]
        public async Task<IActionResult> NovinoCallBack(string paymentStatus, string authority, string invoiceId, int? orderId, int? walletId)
        {
            if (paymentStatus.ToLower() == "ok")
            {
                string correctAuthority = "";
                int price = 0;

                if (walletId.HasValue)
                {
                    var wallet = await walletService.GetWalletClientSideAsync(walletId.Value);

                    if (wallet == null)
                        return NotFound();

                    price = (int)wallet.Price;
                    correctAuthority = wallet.Authority;
                }
                else if (orderId.HasValue)
                {
                    var wallet = await walletService.GetWalletClientSideThroughOrderAsync(orderId.Value);
                    if (wallet != null)
                    {
                        price = (int)wallet.Price;
                        correctAuthority = wallet.Authority;
                    }
                }

                var result = await novinoService.Verifyasync(new NovinoVerifyPaymentRequestDto()
                {
                    Amount = price,
                    Authority = correctAuthority,
                    MerchantId = "test"
                });

                if (result.Status != "100")
                {
                    return View("ErrorPayment", new ErrorPaymentViewModel()
                    {
                        Message = ErrorMessages.ErrorOccuredContactUs,
                        RefId = result.Data.RefId
                    });
                }
                if (walletId.HasValue)
                {
                    await walletService.ClientSideCompletingTheWalletAsync(walletId.Value, result.Data.RefId);
                                       
                    return View("SuccessPayment", new SuccessPaymentViewModel()
                    {
                        Message = SuccessMessages.PaymentDoneSuccessfully,
                        RefId = result.Data.RefId
                    });
                }
                else if (orderId.HasValue)
                {
                    var wallet = await walletService.ClientSideCompletingTheWalletThroughOrderAsync(orderId.Value, result.Data.RefId);
                    #region Add User to Class
                    await orderService.AddUserToClassesAsync((int)orderId, wallet.UserId);
                    #endregion
                    await orderService.ClientSideFinalizingTheOrderAsync(orderId.Value);
                    

                    #region Add Creditor wallet
                    var creditorWallet = new ClientSideWalletAddingOrderViewModel()
                    {
                        IP = wallet.IP,
                        OS = wallet.OS,
                        UserId = wallet.UserId,
                        Price = wallet.Price,
                        OrderId = orderId.Value
                    };
                    var addWalletresult = await walletService.AddingCreditorWalletForFinalizedOrderAsync(creditorWallet);
                    #endregion
                    
                    return View("SuccessPayment", new SuccessPaymentViewModel
                    {
                        Message = SuccessMessages.PaymentDoneSuccessfully,
                        RefId = result.Data.RefId
                    });
                }
            }
            else
            {
                return View("ErrorPayment", new ErrorPaymentViewModel()
                {
                    Message = ErrorMessages.ErrorOccuredContactUs
                });
            }
            return View("ErrorPayment", new ErrorPaymentViewModel()
            {
                Message = ErrorMessages.ErrorOccuredContactUs
            });
        }
    }
}
