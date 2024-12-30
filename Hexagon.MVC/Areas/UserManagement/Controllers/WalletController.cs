using Hexagon.Application.Extensions;
using Hexagon.Application.Services.Interfaces.Wallets;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Wallets;
using Hexagon.MVC.WebExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.UserManagement.Controllers
{
    public class WalletController(IWalletService walletService) : UserManagementBaseSideController
    {
        #region List
        public async Task<IActionResult> List(ClientSideFilterWallet filterWallet)
        {
            var userId = User.GetUserId();
            var walets=await walletService.ClientSideFilterWalletAsync(filterWallet,userId);
            ViewData["Budget"] = await walletService.GetBudgetsAsync(userId);
            return View(walets);
        }
        #endregion

        #region Charging Wallet
        [HttpPost]
        public async Task<IActionResult> ChargeWallet(ClientSideChargingWalletViewModel model)
        {
            if (model.Price < 10000)
            {
                TempData[ErrorMessage] = ErrorMessages.InValidPriceInput;
                return RedirectToAction(nameof(List));
            }
            model.IP = HttpContext.GettingIP();
            model.OS=Environment.OSVersion.GettingOStype();
            model.UserId= User.GetUserId();
            var walletId=await walletService.ClientSideChargingWalletAsync(model);

            return RedirectToAction("StartPaybyNovino", "Payment", new {area="",walletId = walletId});
        }
        #endregion
    }
}
