using Hexagon.Application.Services.Interfaces.Wallets;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Wallets;
using Hexagon.MVC.Utilities;
using Hexagon.MVC.WebExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
{
    public class WalletController(IWalletService walletService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageWallets")]
        public async Task<IActionResult> List(FilterWalletViewModel filter)
        {
            var wallets=await walletService.FilterWalletAsync(filter);
            return View(wallets);
        }
        #endregion

        #region Charge User Wallet
        [HttpGet]
        [AuthorizePermission("ChargeUserWallet")]
        public IActionResult ChargeWallet(int userId)
        {
            return PartialView("_AdminChargeWallet",new AdminChargeWalletViewModel
            {
                UserId = userId
            });
        }
        public async Task<IActionResult> ChargeWallet(AdminChargeWalletViewModel model)
        {
            model.IP = HttpContext.GettingIP();
            model.OS=Environment.OSVersion.GettingOStype();
            var result=await walletService.ChargingWalletAsync(model);
            switch (result)
            {
                case AdminChargeWalletResult.Success:
                    return Ok(new
                    {
                        status=200,
                        message=SuccessMessages.AdminWalletChargedSuccessfully
                    });
            }
            return Ok(new
            {
                status = 204,
                message = ErrorMessages.ErrorOccured
            });
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailWallet")]
        public async Task<IActionResult> Detail(int id)
        {
            var wallet=await walletService.AdminSideDetailWalletAsync(id);
            if(wallet==null)
                return NotFound();

            return View(wallet);
        }
        #endregion
    }
}
