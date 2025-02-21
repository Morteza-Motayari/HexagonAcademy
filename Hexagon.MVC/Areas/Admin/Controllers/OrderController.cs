using Hexagon.Application.Services.Implementation.Wallets;
using Hexagon.Application.Services.Interfaces.Orders;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Orders.ClassesOrder;
using Hexagon.Domain.ViewModels.Wallets;
using Hexagon.MVC.Utilities;
using Hexagon.MVC.WebExtensions;
using Microsoft.AspNetCore.Mvc;

namespace Hexagon.MVC.Areas.Admin.Controllers
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
