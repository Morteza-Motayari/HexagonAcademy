using Hexagon.Domain.Enums.Filter;
using Hexagon.Domain.Enums.Wallets;
using Hexagon.Domain.Interfaces.Wallets;
using Hexagon.Domain.Models.Wallets;
using Hexagon.Domain.ViewModels.Wallets;
using Hexagon.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hexagon.Infra.Data.Repositories.Wallets
{
    public class WalletRepository:GenericRepository<Wallet>,IWalletRepository
    {
        private readonly HexagonContext _db;

        public WalletRepository(HexagonContext db):base(db) 
        {
            _db = db;
        }

        public async Task<ClientSideFilterWallet> ClientSideFilterWalletAsync(ClientSideFilterWallet filter, int userId)

        {
            var query = _db.Wallets.Include(o => o.User).Where(u=>u.UserId==userId).AsQueryable();

            #region Filter Search
            switch (filter.PayementStatus)
            {
                case FilterPayementStatus.All:
                    break;
                case FilterPayementStatus.Payed:
                    query = query.Where(u => u.IsPayed == true);
                    break;
                case FilterPayementStatus.UnPayed:
                    query = query.Where(u => !u.IsPayed);
                    break;
            }

            switch (filter.Case)
            {
                case FilterTransactionCase.All:
                    break;
                case FilterTransactionCase.ChargeWallet:
                    query = query.Where(u => u.Case == TransactionCase.ChargeWallet);
                    break;
                case FilterTransactionCase.PayOrder:
                    query = query.Where(u => u.Case == TransactionCase.PayOrder);
                    break;
            }
            switch (filter.Type)
            {
                case FilterTransactionType.All:
                    break;
                case FilterTransactionType.Deposit:
                    query = query.Where(u => u.Type == TransactionType.Deposit);
                    break;
                case FilterTransactionType.Creditor:
                    query = query.Where(u => u.Type == TransactionType.Creditor);
                    break;
            }
            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new ClientSideWalletViewModel
            {
                Id = u.Id,
                Case = u.Case,
                Type = u.Type,
                IsPayed = u.IsPayed,
                Price = u.Price,
                RefId = u.RefId,
                OrderId = u.OrderId,
                CreatedDate = u.CreatedDate,
                Description = u.Description
            }));
            return filter;
        }

        public async Task<int> CreditorAmount(int userId)
        => (int)await _db.Wallets.Where(w=>w.UserId == userId&&w.IsPayed&&w.Type==TransactionType.Creditor).SumAsync(w=>w.Price);

        public async Task<int> DepositAmount(int userId)
        => (int)await _db.Wallets.Where(w => w.UserId == userId && w.IsPayed && w.Type == TransactionType.Deposit).SumAsync(w => w.Price);

        public async Task<FilterWalletViewModel> FilterWalletAsync(FilterWalletViewModel filter)
        {
            var query = _db.Wallets.Include(o => o.User).AsQueryable();

            #region Filter Search
            switch (filter.PayementStatus)
            {
                case FilterPayementStatus.All:
                    break;
                case FilterPayementStatus.Payed:
                    query = query.Where(u => u.IsPayed == true);
                    break;
                case FilterPayementStatus.UnPayed:
                    query = query.Where(u => !u.IsPayed);
                    break;
            }

            switch (filter.Case)
            {
                case FilterTransactionCase.All:
                    break;
                case FilterTransactionCase.ChargeWallet:
                    query = query.Where(u => u.Case == TransactionCase.ChargeWallet);
                    break;
                case FilterTransactionCase.PayOrder:
                    query = query.Where(u => u.Case == TransactionCase.PayOrder);
                    break;
            }
            switch (filter.Type)
            {
                case FilterTransactionType.All:
                    break;
                case FilterTransactionType.Deposit:
                    query = query.Where(u => u.Type == TransactionType.Deposit);
                    break;
                case FilterTransactionType.Creditor:
                    query = query.Where(u => u.Type == TransactionType.Creditor);
                    break;
            }
            if (filter.UserId.HasValue)
            {
                query = query.Where(u => u.UserId == filter.UserId);
            }
            if (filter.UserName != null)
            {
                string[] search=filter.UserName.Split(' ');
                foreach(string name in search)
                {
                    query = query.Where(r => r.User.FirstName.Contains(name) || r.User.LastName.Contains(name)).Distinct();
                }
            }
            #endregion

            query = query.OrderByDescending(u => u.CreatedDate);

            await filter.Paging(query.Select(u => new WalletViewModel
            {
                UserName=u.User.FirstName+" "+u.User.LastName,
                Id = u.Id,
                IP = u.IP,
                OS = u.OS,
                Case=u.Case,
                Type=u.Type,
                IsPayed=u.IsPayed,
                Price=u.Price,
                RefId=u.RefId,
                OrderId=u.OrderId,
                CreatedDate = u.CreatedDate,
                IsDeleted=u.IsDeleted
            }));
            return filter;
        }

        public async Task<Wallet?> GetWalletByOrderId(int orderId)
        => await _db.Wallets.FirstOrDefaultAsync(w => w.OrderId == orderId);

        public async Task<bool> IsWalletHaveOrder(int walletId)
        => await _db.Wallets.Where(w=>w.Id==walletId).AnyAsync(w=>w.OrderId!=null);

        public async Task UpdateWalletAuthorityAsync(int walletId, string authority)
        => await _db.Wallets.Where(w=>w.Id==walletId)
            .ExecuteUpdateAsync(sp=>sp.SetProperty(w=>w.Authority, authority)
            .SetProperty(w=>w.LastModifiedDate,DateTime.Now));
    }
}
