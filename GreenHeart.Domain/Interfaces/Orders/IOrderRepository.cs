using GreenHeart.Domain.Models.Orders;
using GreenHeart.Domain.ViewModels.Orders.ClassesOrder;
using GreenHeart.Domain.ViewModels.Orders.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Orders
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<bool> ExistUnFinallalizedOrder(int userId);
        Task<Order> GetUnFinallalizedUserOrderId(int userId);
        Task<FilterClientSideOrdersViewModel>GetClientSideUserOrders(int userId);
        Task<FilterOrderViewModel> FilterOrdersAsync(FilterOrderViewModel filter);
        Task<int> GetTotalOrderPriceAsync(int orderId);
        Task FinalizingOrderAsync(int orderId);
        Task<DateTime> GetLastModifiedDate(int id);
    }
}
