using Hexagon.Domain.ViewModels.Orders.ClassesOrder;
using Hexagon.Domain.ViewModels.Orders.Orders;
using Hexagon.Domain.ViewModels.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Services.Interfaces.Orders
{
    public interface IOrderService
    {
        Task<CreateClassOrderResult> CreateClassOrderAsync(CreateClassOrderViewModel model);
        Task<DeleteClassOrderResult> DeleteClassOrderAsync(int ClassOrderId);
        Task<UpdateOrderResult> FinalizingTheOrderAsync(UpdateOrderViewModel model);
        Task<FilterClientSideOrdersViewModel> ClinetSideOrdersAsync(int userId);
        Task<ClientSideOrderDetailViewModel?> ClientSideViewOrderDetailAsync(int orderId);
        Task<ClientSidePayOrderViewModel?> ClientSideOrderDetailAsync(int orderId);
        Task<ClientSideOrderDetailViewModel?> ClientSideGetOrderForPayAsync(int orderId);
        Task<FilterOrderViewModel> FilterOrderAsync(FilterOrderViewModel filter);
        Task<AdminSideDetailOrderViewModel> GetOrderDetailsAsync(int orderId);
        Task<ClientSideOrderViewModel?> GetOrderClientSideAsync(int orderId);
        Task ClientSideFinalizingTheOrderAsync(int orderId);
        Task<bool> CheckFinalizedOrderFormClassOrderAsync(int classOrderId);
        Task<ClientSidePayOrderResult> ClientSidePayOrderAsync(ClientSidePayOrderViewModel model);
        Task AddUserToClassesAsync(int orderId,int userId);
        Task<string> UnRegisterAbleClasses(int userId, int orderId);
        Task<string> FilledClasses(int userId, int orderId, ICollection<ClientSideClassOrderDetail>? ClassesOrder);
        Task<List<ClientSideClassOrderDetail>?> GetOrderClassesForOrderAsync(int orderId);
    }
}
