using Hexagon.Domain.Models.Wallets;
using Hexagon.Domain.ViewModels.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.Interfaces.Wallets
{
    public interface IWalletRepository:IGenericRepository<Wallet>
    {
        Task<FilterWalletViewModel> FilterWalletAsync(FilterWalletViewModel filter);
        Task<ClientSideFilterWallet> ClientSideFilterWalletAsync(ClientSideFilterWallet filter,int userId);
        Task<int> CreditorAmount(int userId);
        Task<int> DepositAmount(int userId);
        Task UpdateWalletAuthorityAsync(int walletId, string authority);
        Task<Wallet?> GetWalletByOrderId(int orderId);
        Task<bool> IsWalletHaveOrder(int walletId);
    }
}
