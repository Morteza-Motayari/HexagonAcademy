using Hexagon.Application.Services.Interfaces.Wallets;
using Hexagon.Domain.Enums.Wallets;
using Hexagon.Domain.Interfaces;
using Hexagon.Domain.Interfaces.Wallets;
using Hexagon.Domain.Models.Wallets;
using Hexagon.Domain.Shared;
using Hexagon.Domain.ViewModels.Wallets;
using Hexagon.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Services.Implementation.Wallets
{
    public class WalletService(IWalletRepository walletRepository
        ,IUserRepository userRepository) : IWalletService
    {
        public async Task<ClientSideWalletAddingOrderResult> AddingCreditorWalletForFinalizedOrderAsync(ClientSideWalletAddingOrderViewModel model)
        {

            Wallet wallet = new()
            {
                Case = TransactionCase.PayOrder,
                Description = InfoMessages.PayOrder,
                IP=model.IP,
                OS=model.OS,
                OrderId=model.OrderId,
                IsPayed=true,
                Price=model.Price,
                UserId=model.UserId,
                RefId=null,
                Type=TransactionType.Creditor
            };
            await walletRepository.InserAsync(wallet);
            await walletRepository.SaveChangeAsync();
            return ClientSideWalletAddingOrderResult.success;
        }

        public async Task<AdminSideDetailWalletViewModel> AdminSideDetailWalletAsync(int walletId)
        {
            var wallet=await walletRepository.GetByIdAsync(walletId);
            if (wallet == null)
                return null;
            return new AdminSideDetailWalletViewModel()
            {
                Id = wallet.Id,
                UserId = wallet.UserId,
                Description = wallet.Description,
                IP = wallet.IP,
                OS = wallet.OS,
                RefId = wallet.RefId,
                OrderId = wallet.OrderId,
                IsPayed = wallet.IsPayed,
                Case = wallet.Case,
                Type = wallet.Type,
                Price = wallet.Price,
                Authority=wallet.Authority,
                UserName= await userRepository.GetJustUserName(wallet.UserId),
                IsDeleted = wallet.IsDeleted,
                CreatedDate = wallet.CreatedDate,
                LastModifiedDate = wallet.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(wallet.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(wallet.LastModifiedBy),
                CreatedById = wallet.CreatedBy,
                LastModifiedById = wallet.LastModifiedBy
            };
        }

        public async Task<AdminChargeWalletResult> ChargingWalletAsync(AdminChargeWalletViewModel model)
        {
            Wallet wallet = new()
            {
                Price = model.Price,
                Case = model.Case,
                Type = model.Type,
                Description = model.Description,
                IP = model.IP,
                OS = model.OS,
                RefId = model.RefId,
                IsPayed = true,
                UserId = model.UserId
            };
            await walletRepository.InserAsync(wallet);
            await walletRepository.SaveChangeAsync();
            return AdminChargeWalletResult.Success;
        }

        public async Task<int> ClientSideChargingWalletAsync(ClientSideChargingWalletViewModel model)
        {
            Wallet wallet = new()
            {
                Price = model.Price,
                IP = model.IP,
                OS = model.IP,
                RefId = model.RefId,
                Description = model.Description,
                Case = TransactionCase.ChargeWallet,
                Type = TransactionType.Deposit,
                UserId = model.UserId,
                IsPayed=false
            };
            await walletRepository.InserAsync(wallet);
            await walletRepository.SaveChangeAsync();
            return wallet.Id;
        }

        public async Task ClientSideCompletingTheWalletAsync(int walletId, string refId)
        {
            var wallet=await walletRepository.GetByIdAsync(walletId);
            if(wallet != null)
            {
                wallet.RefId = refId;
                wallet.IsPayed = true;
                walletRepository.Update(wallet);
                await walletRepository.SaveChangeAsync();
            }
        }

        public async Task<ClientSideWalletAddingOrderViewModel?> ClientSideCompletingTheWalletThroughOrderAsync(int orderId, string refId)
        {
            var wallet = await walletRepository.GetWalletByOrderId(orderId);
            if (wallet != null)
            {
                wallet.RefId = refId;
                wallet.IsPayed = true;
                walletRepository.Update(wallet);
                await walletRepository.SaveChangeAsync();

                return new ClientSideWalletAddingOrderViewModel
                {
                    Id = wallet.Id,
                    IP = wallet.IP,
                    OS = wallet.OS,
                    OrderId = orderId,
                    Price = wallet.Price,
                    UserId = wallet.UserId
                };
            }
            return null;
        }

        public async Task<ClientSideFilterWallet> ClientSideFilterWalletAsync(ClientSideFilterWallet filter, int userId)
        => await walletRepository.ClientSideFilterWalletAsync(filter, userId);

        public async Task ClientSideUpdateWalletAuthorityAsync(int walletId, string authority)
        => await walletRepository.UpdateWalletAuthorityAsync(walletId, authority);

        public async Task ClientSideUpdateWalletAuthorityThroughOrderAsync(int orderId, string authority)
        {
            var wallet=await walletRepository.GetWalletByOrderId(orderId);

            if(wallet != null)
            await walletRepository.UpdateWalletAuthorityAsync(wallet.Id, authority);

        }

        public async Task<FilterWalletViewModel> FilterWalletAsync(FilterWalletViewModel filter)
        => await walletRepository.FilterWalletAsync(filter);

        public async Task<int> GetBudgetsAsync(int userId)
        {
            int credit=await walletRepository.CreditorAmount(userId); 
            int deposit=await walletRepository.DepositAmount(userId);
            return deposit-credit;
        }

        public async Task<ClientSideWalletViewModel?> GetWalletClientSideAsync(int walletId)
        {
            var wallet= await walletRepository.GetByIdAsync(walletId);
            if (wallet == null)
                return null;
            return new ClientSideWalletViewModel
            {
                Id = wallet.Id,
                Case = wallet.Case,
                CreatedDate = wallet.CreatedDate,
                Description = wallet.Description,
                IsPayed = wallet.IsPayed,
                OrderId = wallet.OrderId,
                Price = wallet.Price,
                RefId = wallet.RefId,
                Type = wallet.Type,
                Authority = wallet.Authority
            };
        }

        public async Task<ClientSideWalletViewModel?> GetWalletClientSideThroughOrderAsync(int orderId)
        {
            var wallet = await walletRepository.GetWalletByOrderId(orderId);
            if (wallet == null)
                return null;
            return new ClientSideWalletViewModel
            {
                Id = wallet.Id,
                Case = wallet.Case,
                CreatedDate = wallet.CreatedDate,
                Description = wallet.Description,
                IsPayed = wallet.IsPayed,
                OrderId = wallet.OrderId,
                Price = wallet.Price,
                RefId = wallet.RefId,
                Type = wallet.Type,
                Authority = wallet.Authority
            };
        }

        public async Task<bool> IsWalletHaveOrderAsync(int walletId)
        => await walletRepository.IsWalletHaveOrder(walletId);
    }
}
