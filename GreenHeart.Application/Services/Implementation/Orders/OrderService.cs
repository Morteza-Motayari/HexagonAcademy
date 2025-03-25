using GreenHeart.Application.Extensions;
using GreenHeart.Application.Generators;
using GreenHeart.Application.Services.Interfaces.Orders;
using GreenHeart.Domain.Enums.Payment;
using GreenHeart.Domain.Enums.SportClasses;
using GreenHeart.Domain.Enums.Wallets;
using GreenHeart.Domain.Interfaces;
using GreenHeart.Domain.Interfaces.Gyms;
using GreenHeart.Domain.Interfaces.Links;
using GreenHeart.Domain.Interfaces.Orders;
using GreenHeart.Domain.Interfaces.Wallets;
using GreenHeart.Domain.Models.Links;
using GreenHeart.Domain.Models.Orders;
using GreenHeart.Domain.Models.Wallets;
using GreenHeart.Domain.Shared;
using GreenHeart.Domain.ViewModels.Orders.ClassesOrder;
using GreenHeart.Domain.ViewModels.Orders.Orders;
using Microsoft.AspNetCore.Http;

namespace GreenHeart.Application.Services.Implementation.Orders
{
    public class OrderService(IOrderRepository orderRepository
        , IClassOrderRepository classOrderRepository
        , IUserRepository userRepository
        , IHttpContextAccessor httpContextAccessor
        , IWalletRepository walletRepository
        , IClassUserRepository classUserRepository
        , ISportClassRepository sportClassRepository) : IOrderService
    {
        public async Task AddUserToClassesAsync(int orderId, int userId)
        {
            var getOrderClassesId = await classOrderRepository.GetUserOrderClassesIdForRegistration(userId, orderId);
            if (getOrderClassesId.CheckNullability())
            {
                foreach (var ClassId in getOrderClassesId)
                {
                    ClassUser classUser = new()
                    {
                        SportClassId = ClassId,
                        UserId = userId,
                        SubscriptionDate = DateTime.Now
                    };
                    await classUserRepository.InserAsync(classUser);
                }
                await classUserRepository.SaveChangeAsync();
            }            
        }

        public async Task<AdminSideDetailOrderViewModel> AdminSideDetailOrderAsync(int orderId)
        {
            var order = await orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;
            return new AdminSideDetailOrderViewModel()
            {
                Id = order.Id,
                IsFainally=order.IsFainally,
                TotalPrice= await orderRepository.GetTotalOrderPriceAsync(orderId),
                IsDeleted = order.IsDeleted,
                Wallets=await walletRepository.GetOrderWalletsViewModel(orderId),
                ClassesOrder= await classOrderRepository.GetClassesOrderAdminDetailViewModel(orderId),
                CreatedDate = order.CreatedDate,
                LastModifiedDate = order.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(order.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(order.LastModifiedBy),
                CreatedById = order.CreatedBy,
                LastModifiedById = order.LastModifiedBy
            };
        }

        public async Task<bool> CheckFinalizedOrderFormClassOrderAsync(int classOrderId)
        => await classOrderRepository.CheckFinalizedOrderfromClassOrder(classOrderId);

        public async Task ClientSideFinalizingTheOrderAsync(int orderId)
        => await orderRepository.FinalizingOrderAsync(orderId);

        public async Task<ClientSideOrderDetailViewModel?> ClientSideGetOrderForPayAsync(int orderId)
        {
            var order = await orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;
            return new ClientSideOrderDetailViewModel
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                ClassesOrder = await classOrderRepository.GetClassesOrderDetailAsync(orderId),
                IsFainally = order.IsFainally,
                TotalPrice = await orderRepository.GetTotalOrderPriceAsync(orderId),
                PayedDate = order.LastModifiedDate ?? order.CreatedDate
            };

        }

        public async Task<ClientSidePayOrderViewModel?> ClientSideOrderDetailAsync(int orderId)
        {
            var order = await orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;
            return new ClientSidePayOrderViewModel
            {
                Id = order.Id,
                ClassesOrder = await classOrderRepository.GetClassesOrderDetailAsync(orderId),
                IsFainally = order.IsFainally,
                TotalPrice = await orderRepository.GetTotalOrderPriceAsync(orderId),
            };
        }

        public async Task<ClientSidePayOrderResult> ClientSidePayOrderAsync(ClientSidePayOrderViewModel model)
        {
            if (!CaptchaGenerator.ValidateCaptchaCode(model.CaptchaCode, httpContextAccessor.HttpContext))
            {
                return ClientSidePayOrderResult.InValidCaptcha;
            }
            if (model.ClassesOrder.CheckNullability())
            {
                foreach (var item in model.ClassesOrder)
                {
                    if (await sportClassRepository.GetMaxClassAthleteSpace(item.ClassId) <= await classUserRepository.RegisteredUserLastMonth(item.ClassId))
                    {
                        //We delete this classes from order after getting the error message


                        //var classOrder = await classOrderRepository.GetUserClassOrder(item.Id, model.UserId);
                        //classOrderRepository.Delete(classOrder);
                        //await classOrderRepository.SaveChangeAsync();
                        return ClientSidePayOrderResult.ClassSapceFilled;
                    }
                    if (item.ClassStatus != SportClassStatus.Active)
                    {
                        //We delete this classes from order after getting the error message 


                        //var classOrder = await classOrderRepository.GetUserClassOrder(item.Id, model.UserId);
                        //classOrderRepository.Delete(classOrder);
                        //await classOrderRepository.SaveChangeAsync();
                        return ClientSidePayOrderResult.ClassCantRegistered;
                    }
                }
            }
            if (model.Payment == PaymentType.Wallet)
            {

                int AccountBalance = await walletRepository.DepositAmount(model.UserId) - await walletRepository.CreditorAmount(model.UserId);
                if (model.TotalPrice > AccountBalance)
                {
                    return ClientSidePayOrderResult.InSufficintWalletMoney;
                }
                Wallet OrderFee = new()
                {
                    OrderId = model.Id,
                    Case = TransactionCase.PayOrder,
                    Type = TransactionType.Creditor,
                    Price = model.TotalPrice,
                    Description = InfoMessages.PayOrderFromWallet,
                    IP = model.IP,
                    OS = model.OS,
                    IsPayed = true,
                    UserId = model.UserId
                };
                await walletRepository.InserAsync(OrderFee);
                await walletRepository.SaveChangeAsync();

                await AddUserToClassesAsync(model.Id,model.UserId);

                await orderRepository.FinalizingOrderAsync(model.Id);
                return ClientSidePayOrderResult.SuccessFromWallet;
            }
            else
            {
                Wallet PayOrder = new()
                {
                    OrderId = model.Id,
                    Case = TransactionCase.PayOrder,
                    Type = TransactionType.Deposit,
                    Price = model.TotalPrice,
                    Description = InfoMessages.ChargeForPayOrder,
                    IP = model.IP,
                    OS = model.OS,
                    IsPayed = false,
                    UserId = model.UserId
                };
                await walletRepository.InserAsync(PayOrder);
                await walletRepository.SaveChangeAsync();

                return ClientSidePayOrderResult.SuccessGoToGateWay;
            }
            
        }

        public async Task<ClientSideOrderDetailViewModel?> ClientSideViewOrderDetailAsync(int orderId)
        {
            var order = await orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;
            return new ClientSideOrderDetailViewModel
            {
                Id = order.Id,
                ClassesOrder = await classOrderRepository.GetClassesOrderDetailAsync(orderId),
                IsFainally = order.IsFainally,
                TotalPrice = await orderRepository.GetTotalOrderPriceAsync(orderId),
                PayedDate=(DateTime)order.LastModifiedDate
            };
        }

        public async Task<FilterClientSideOrdersViewModel> ClinetSideOrdersAsync(int userId)
        => await orderRepository.GetClientSideUserOrders(userId);

        public async Task<CreateClassOrderResult> CreateClassOrderAsync(CreateClassOrderViewModel model)
        {
            if (await userRepository.GetUserGenderAsync(model.UserId) != model.Gender)
            {
                return CreateClassOrderResult.InCorrectGender;
            }
            Order order = default;
            if (await orderRepository.ExistUnFinallalizedOrder(model.UserId))
            {
                order = await orderRepository.GetUnFinallalizedUserOrderId(model.UserId);
                if (await classOrderRepository.ExistClassForOrder(order.Id, model.ClassId))
                {
                    return CreateClassOrderResult.ClassAlreadyRegistered;
                }
            }
            else
            {
                order = new()
                {
                    IsFainally = false
                };
                await orderRepository.InserAsync(order);
                await orderRepository.SaveChangeAsync();
            }
            ClassOrder classOrder = new()
            {
                OrderId = order.Id,
                ClassId = model.ClassId,
                Price = model.Price
            };
            await classOrderRepository.InserAsync(classOrder);
            await classOrderRepository.SaveChangeAsync();
            return CreateClassOrderResult.Success;
        }

        public async Task<DeleteClassOrderResult> DeleteClassOrderAsync(int ClassOrderId)
        {
            var classOrder = await classOrderRepository.GetByIdAsync(ClassOrderId);
            if (classOrder == null)
                return DeleteClassOrderResult.OrderNotFound;
            classOrderRepository.Delete(classOrder);
            await classOrderRepository.SaveChangeAsync();
            return DeleteClassOrderResult.Success;
        }

        public async Task<string> FilledClasses(int userId, int orderId, ICollection<ClientSideClassOrderDetail>? ClassesOrder)
        {
            string classesName = " امکان شرکت در کلاس" ;
            int i = 0;
            foreach (var item in ClassesOrder)
            {
                if (await sportClassRepository.GetMaxClassAthleteSpace(item.ClassId) <= await classUserRepository.RegisteredUserLastMonth(item.ClassId))
                {
                    var className=await sportClassRepository.getSportClassName(item.ClassId);
                    if (i == 0)
                    {
                        classesName = classesName + className;
                    }
                    else
                    {
                        classesName = classesName + " و " + className;
                    }
                    i++;
                    var classOrder = await classOrderRepository.GetUserClassOrder(item.ClassId, userId);
                    classOrderRepository.Delete(classOrder);
                }
            }
            await classOrderRepository.SaveChangeAsync();
            classesName = classesName + "به دلیل پربودن ظرفیت فعلا وجود ندارد.";
            return classesName;
        }

        public async Task<FilterOrderViewModel> FilterOrderAsync(FilterOrderViewModel filter)
        => await orderRepository.FilterOrdersAsync(filter);

        public async Task<UpdateOrderResult> FinalizingTheOrderAsync(UpdateOrderViewModel model)
        {
            var order = await orderRepository.GetByIdAsync(model.Id);
            if (order == null)
                return UpdateOrderResult.OrderNotFound;
            order.IsFainally = true;
            orderRepository.Update(order);
            await orderRepository.SaveChangeAsync();
            return UpdateOrderResult.Success;
        }

        public async Task<List<ClientSideClassOrderDetail>?> GetOrderClassesForOrderAsync(int orderId)
        => await classOrderRepository.GetClassesOrderDetailAsync(orderId);

        public async Task<ClientSideOrderViewModel?> GetOrderClientSideAsync(int orderId)
        {
            var order = await orderRepository.GetByIdAsync(orderId);
            if (order == null)
                return null;
            return new ClientSideOrderViewModel
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                IsFainally = order.IsFainally,
                TotalPrice = await orderRepository.GetTotalOrderPriceAsync(orderId)
            };
        }

        public async Task<AdminSideDetailOrderViewModel> GetOrderDetailsAsync(int orderId)
        {
            var orderDetail = await orderRepository.GetByIdAsync(orderId);
            if (orderDetail == null)
                return null;
            AdminSideDetailOrderViewModel detail = new()
            {
                Id = orderDetail.Id,
                IsFainally = orderDetail.IsFainally,
                CreatedDate = orderDetail.CreatedDate,
                LastModifiedDate = orderDetail.LastModifiedDate,
                CreatedBy = await userRepository.GetJustUserName(orderDetail.CreatedBy),
                LastModifiedBy = await userRepository.GetJustUserName(orderDetail.LastModifiedBy),
                CreatedById = orderDetail.CreatedBy,
                LastModifiedById = orderDetail.LastModifiedBy,
                IsDeleted = orderDetail.IsDeleted
            };
            return detail;
        }

        public async Task<string> UnRegisterAbleClasses(int userId,int orderId)
        {
            var claases= await classOrderRepository.GetUnRegisterAbleClassesName(userId,orderId);
            string classesName =claases.Count()==1? " امکان شرکت در": " امکان شرکت در کلاس های " ;
            int i = 0;
            foreach (var item in claases)
            {
                if(i == 0)
                {
                    classesName = classesName + item.ClassName;
                }
                else
                {
                    classesName=classesName+" و "+item.ClassName;
                }
                i++;
                var classOrder = await classOrderRepository.GetUserClassOrder(item.ClassId, userId);
                classOrderRepository.Delete(classOrder);
            }
            await classOrderRepository.SaveChangeAsync();
            classesName = classesName + " فعلا وجود ندارد.";
            return classesName;
        }
    }
}
