using GreenHeart.Domain.ViewModels.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Interfaces.Wallets
{
    public interface IWalletService
    {
        Task<AdminChargeWalletResult> ChargingWalletAsync(AdminChargeWalletViewModel model);
        Task<AdminSideDetailWalletViewModel> AdminSideDetailWalletAsync(int walletId);
        Task<FilterWalletViewModel> FilterWalletAsync(FilterWalletViewModel filter);
        Task<int> ClientSideChargingWalletAsync(ClientSideChargingWalletViewModel model);
        Task<ClientSideFilterWallet> ClientSideFilterWalletAsync(ClientSideFilterWallet filter,int userId);
        Task<int> GetBudgetsAsync(int userId);
        Task<ClientSideWalletViewModel?> GetWalletClientSideAsync(int walletId);
        Task<ClientSideWalletViewModel?> GetWalletClientSideThroughOrderAsync(int orderId);
        Task ClientSideUpdateWalletAuthorityAsync(int walletId, string authority);
        Task ClientSideUpdateWalletAuthorityThroughOrderAsync(int orderId, string authority);
        Task ClientSideCompletingTheWalletAsync(int walletId,string refId);
        Task<ClientSideWalletAddingOrderViewModel?> ClientSideCompletingTheWalletThroughOrderAsync(int orderId,string refId);
        Task<ClientSideWalletAddingOrderResult> AddingCreditorWalletForFinalizedOrderAsync(ClientSideWalletAddingOrderViewModel model);
        Task<bool> IsWalletHaveOrderAsync(int walletId);
    }
}
