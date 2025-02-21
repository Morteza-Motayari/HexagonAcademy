using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Orders;
using Hexagon.Domain.Models.Orders;
using Hexagon.Domain.Models.Wallets;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Orders.ClassesOrder;
using Hexagon.Domain.ViewModels.Orders.Orders;
using Hexagon.MVC.Utilities.ActionFilters;
using Hexagon.MVC.WebExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.UserManagement.Controllers
{
    public class OrderController(IOrderService orderService) : UserManagementBaseSideController
    {
        #region List
        [ServiceFilter(typeof(UserInfoFilter))]
        public async Task<IActionResult> List()
        {
            var orders = await orderService.ClinetSideOrdersAsync(User.GetUserId());
            ViewData["Title"] = Titles.UserOrders;
            return View(orders);
        }
        #endregion

        #region Create ClassOrder
        [HttpPost]
        [ServiceFilter(typeof(UserInfoFilter))]
        public async Task<IActionResult> AddClassToOrder(CreateClassOrderViewModel model)
        {
            model.UserId = User.GetUserId();
            var result = await orderService.CreateClassOrderAsync(model);
            switch (result)
            {
                case CreateClassOrderResult.Success:
                    TempData[SuccessMessage] = SuccessMessages.ClassOrderAddSuccessFullyDone;
                    return RedirectToAction(nameof(List), "Order", new { area = "UserManagement" });
                case CreateClassOrderResult.ClassAlreadyRegistered:
                    TempData[ErrorMessage] = ErrorMessages.ClassOrderAlreadyRegistered;
                    break;
                case CreateClassOrderResult.InCorrectGender:
                    TempData[ErrorMessage] = ErrorMessages.IncorrectClassForYou;
                    break;
            }
            return RedirectToAction($"{model.ClassSlug}", "Class", new { area = "" });
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int orderId)
        {
            var order = await orderService.ClientSideViewOrderDetailAsync(orderId);
            if (order == null)
                return NotFound();

            ViewData["Title"] = Titles.UserOrderDetail;
            return View(order);
        }
        #endregion

        #region Delete Class Order
        public async Task Delete(int id)
        {
            if(!await orderService.CheckFinalizedOrderFormClassOrderAsync(id))
            {
                var result = await orderService.DeleteClassOrderAsync(id);
            }            
        }
        #endregion

        #region Pay Order 
        [HttpGet]
        [ServiceFilter(typeof(UserInfoFilter))]
        public async Task<IActionResult> PayOrder(int orderId)
        {
            var order=await orderService.ClientSideOrderDetailAsync(orderId);
            if (order.TotalPrice == 0)
            {
                TempData[WarningMessage] = WarningMessages.OrderHaveNoItem;
                return RedirectToAction(nameof(List));
            }
            if (order == null)
                return NotFound();
            if (order.IsFainally == true)
            {
                TempData[WarningMessage] = WarningMessages.OrderAlreadyPayed;
                return View(nameof(List));
            }
       
            return View(order);

        }
        [HttpPost]
        public async Task<IActionResult> PayOrder(ClientSidePayOrderViewModel model)
        {          
            model.IP = HttpContext.GettingIP();
            model.OS = Request.Headers["User-Agent"].GettingClientOSType();
            model.UserId=User.GetUserId();
            model.ClassesOrder=await orderService.GetOrderClassesForOrderAsync(model.Id);
            var result = await orderService.ClientSidePayOrderAsync(model);
            switch(result)
            {
               case ClientSidePayOrderResult.SuccessGoToGateWay:
                    return RedirectToAction("StartPaybyNovino", "Payment", new { area = "", orderId = model.Id });
                case ClientSidePayOrderResult.SuccessFromWallet:
                    TempData[SuccessMessage] = SuccessMessages.OrderPayedSuccessFully;
                    return RedirectToAction(nameof(List));
                case ClientSidePayOrderResult.ClassCantRegistered:
                    string message = await orderService.UnRegisterAbleClasses(model.UserId, model.Id);
                    TempData[ErrorMessage] = message;
                        break;
                case ClientSidePayOrderResult.ClassSapceFilled:
                    string message2 = await orderService.FilledClasses(model.UserId, model.Id, model.ClassesOrder);
                    TempData[ErrorMessage] = message2;
                        break;
                case ClientSidePayOrderResult.InValidCaptcha:
                    TempData[ErrorMessage] = ErrorMessages.InvalidCaptcha;
                        break;
                case ClientSidePayOrderResult.InSufficintWalletMoney:
                    TempData[ErrorMessage] = ErrorMessages.InsufficientWalletmoney;
                        break;
            }
            return View(model);
        }
        #endregion
    }
}
