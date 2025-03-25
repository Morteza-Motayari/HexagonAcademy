using GreenHeart.Domain.Models.Orders;
using GreenHeart.Domain.ViewModels.Orders.ClassesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.Interfaces.Orders
{
    public interface IClassOrderRepository:IGenericRepository<ClassOrder>
    {
        Task<bool> ExistClassForOrder(int orderId,int classId);
        Task<List<ClientSideClassOrderDetail>?> GetClassesOrderDetailAsync(int orderId);
        Task<bool> CheckFinalizedOrderfromClassOrder(int classOrderId);
        Task<ClassOrder> GetUserClassOrder(int classId,int userId);
        Task<List<int>?> GetUserOrderClassesIdForRegistration(int userId, int orderId);
        Task<List<DeleteClassOrderViewModel>> GetUnRegisterAbleClassesName(int userId,int orderId);
        Task<DateTime> GetLastModifiedDate(int id);
        Task<List<ClassOrderDetailViewModel>> GetClassesOrderAdminDetailViewModel(int orderId);
    }
}
