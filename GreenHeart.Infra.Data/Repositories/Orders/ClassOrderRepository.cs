using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.Interfaces.Orders;
using GreenHeart.Domain.Models.Orders;
using GreenHeart.Domain.ViewModels.Orders.ClassesOrder;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Repositories.Orders
{
    public class ClassOrderRepository:GenericRepository<ClassOrder>,IClassOrderRepository
    {
        private readonly GreenHeartContext _db;

        public ClassOrderRepository(GreenHeartContext db):base(db)
        {
           _db = db;
        }

        public async Task<bool> CheckFinalizedOrderfromClassOrder(int classOrderId)
        => await _db.ClassOrders.Include(co=>co.order).Where(co=>co.Id==classOrderId)
            .Select(co=>co.order.IsFainally).FirstAsync();

        public async Task<bool> ExistClassForOrder(int orderId, int classId)
        => await _db.ClassOrders.AnyAsync(co=>co.OrderId==orderId&&co.ClassId==classId);

        public async Task<List<ClientSideClassOrderDetail>?> GetClassesOrderDetailAsync(int orderId)
        => await _db.ClassOrders.Include(co=>co.Class).Where(o=>o.OrderId==orderId)
            .Select(co=>new ClientSideClassOrderDetail
            {
                Id = co.Id,
                OrderId = orderId,
                ClassId = co.ClassId,
                ClassName=co.Class.Title,
                ClassSlug=co.Class.Slug,
                CreatedDate = co.CreatedDate,
                Price = co.Price,
                ClassStatus=co.Class.ClassStatus
            }).ToListAsync();

        public async Task<List<DeleteClassOrderViewModel>> GetUnRegisterAbleClassesName(int userId, int orderId)
        => await _db.ClassOrders.Include(co=>co.Class).Where(co=>co.CreatedBy==userId&&co.OrderId==orderId&&co.Class.ClassStatus!=SportClassStatus.Active).Select(s=>new DeleteClassOrderViewModel
        {
            Id=s.Id,
            ClassId=s.ClassId,
            ClassName =s.Class.Title
        }).ToListAsync();

        public async Task<ClassOrder> GetUserClassOrder(int classId,int userId)
        => await _db.ClassOrders.Include(c=>c.order).Where(co=>co.order.IsFainally==false&&co.ClassId==classId&&co.CreatedBy==userId).FirstAsync();

        public async Task<List<int>?> GetUserOrderClassesIdForRegistration(int userId, int orderId)
        => await _db.ClassOrders.Where(co=>co.CreatedBy==userId&&co.OrderId==orderId).Select(s=>s.ClassId).ToListAsync();

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.ClassOrders.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();

        public async Task<List<ClassOrderDetailViewModel>> GetClassesOrderAdminDetailViewModel(int orderId)
        => await _db.ClassOrders.Include(co => co.Class).Where(o => o.OrderId == orderId)
            .Select(co => new ClassOrderDetailViewModel
            {
                Id = co.Id,
                OrderId = orderId,
                ClassId = co.ClassId,
                ClassName = co.Class.Title,
                CreatedDate = co.CreatedDate,
                Price = co.Price,
                ClassStatus = co.Class.ClassStatus
            }).ToListAsync();
    }
}
