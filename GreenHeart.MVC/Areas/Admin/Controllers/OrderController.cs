using GreenHeart.Application.Services.Implementation.Wallets;
using GreenHeart.Application.Services.Interfaces.Orders;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Orders.ClassesOrder;
using GreenHeart.Domain.ViewModels.Wallets;
using GreenHeart.MVC.Utilities;
using GreenHeart.MVC.WebExtensions;
using Microsoft.AspNetCore.Mvc;

namespace GreenHeart.MVC.Areas.Admin.Controllers
{
    public class OrderController(IOrderService orderService) : AdminSideController
    {
        #region List
        [AuthorizePermission("ManageOrders")]
        public async Task<IActionResult> List(FilterOrderViewModel filter)
        {
            var orders = await orderService.FilterOrderAsync(filter);
            ViewData["Title"] = Titles.AdminOrders;
            return View(orders);
        }
        #endregion

        #region Detail
        [AuthorizePermission("DetailOrder")]
        public async Task<IActionResult> Detail(int id)
        {
            var order = await orderService.AdminSideDetailOrderAsync(id);
            if (order == null)
                return NotFound();
            ViewData["Title"] = Titles.AdminDetailOrder;
            return View(order);
        }
        #endregion

    }
}
