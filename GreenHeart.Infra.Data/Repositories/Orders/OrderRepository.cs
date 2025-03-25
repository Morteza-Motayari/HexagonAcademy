using GreenHeart.Domain.Enums.Filter;
using GreenHeart.Domain.Interfaces.Orders;
using GreenHeart.Domain.Models.Orders;
using GreenHeart.Domain.ViewModels.Orders.ClassesOrder;
using GreenHeart.Domain.ViewModels.Orders.Orders;
using GreenHeart.Domain.ViewModels.Records.Certificates;
using GreenHeart.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Infra.Data.Repositories.Orders
{
    public class OrderRepository:GenericRepository<Order>,IOrderRepository
    {
        private readonly GreenHeartContext _db;

        public OrderRepository(GreenHeartContext db):base(db)
        {
           _db = db;
        }

        public async Task<bool> ExistUnFinallalizedOrder(int userId)
        => await _db.Orders.AnyAsync(u=>u.CreatedBy == userId&&!u.IsFainally);

        public async Task<FilterOrderViewModel> FilterOrdersAsync(FilterOrderViewModel filter)
        {
            var query = _db.Orders.Include(o=>o.User).Include(o=>o.ClassesOrder).AsQueryable();

            #region Filter Search
            switch (filter.OrderStatus)
            {
                case FilterOrderStatus.All:
                    break;
                case FilterOrderStatus.Payed:
                    query = query.Where(u => u.IsFainally == true);
                    break;
                case FilterOrderStatus.UnPayed:
                    query = query.Where(u => !u.IsFainally);
                    break;
            }

            if (filter.UserName != null)
            {
                string[] search = filter.UserName.Split(' ');
                foreach (string name in search)
                {
                    query = query.Where(r => r.User.FirstName.Contains(name) || r.User.LastName.Contains(name)).Distinct();
                }
            }

            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new OrderViewModel
            {
                Id = u.Id,
                CreatedDate = u.CreatedDate,
                IsFainally = u.IsFainally,
                TotalPrice=u.ClassesOrder.Sum(o=>o.Price),
                UserId=u.CreatedBy,
                UserName=u.User.FirstName+" "+u.User.LastName
            }));
            return filter;
        }

        public async Task FinalizingOrderAsync(int orderId)
        => await _db.Orders.Where(o=>o.Id == orderId)
            .ExecuteUpdateAsync(sp=>sp.SetProperty(p=>p.IsFainally,true)
            .SetProperty(p=>p.LastModifiedDate,DateTime.Now));

        public async Task<FilterClientSideOrdersViewModel> GetClientSideUserOrders(int userId)
        {
            var query = _db.Orders.Include(o=>o.ClassesOrder).Where(o=>o.CreatedBy==userId&&!o.IsDeleted).AsQueryable();

            #region Filter Search

            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);
            FilterClientSideOrdersViewModel filter = new();
            await filter.Paging(query.Select(u => new ClientSideOrderViewModel
            {
                Id = u.Id,
                CreatedDate = u.CreatedDate,
                IsFainally = u.IsFainally,
                TotalPrice=u.ClassesOrder.Sum(o=>o.Price),
                LastModifiedDate=u.LastModifiedDate??u.CreatedDate
            }));
            return filter;
        }

        public async Task<int> GetTotalOrderPriceAsync(int orderId)
        => await _db.Orders.Include(o=>o.ClassesOrder).Where(o=>o.Id==orderId)
            .Select(p => p.ClassesOrder.Sum(o=>o.Price)).FirstAsync();

        public async Task<Order> GetUnFinallalizedUserOrderId(int userId)
        => await _db.Orders.Where(o=>o.CreatedBy==userId&&!o.IsFainally).FirstAsync();

        public async Task<DateTime> GetLastModifiedDate(int id)
        => await _db.Orders.Where(d => d.Id == id).Select(s => (DateTime)s.LastModifiedDate).FirstAsync();
    }
}
