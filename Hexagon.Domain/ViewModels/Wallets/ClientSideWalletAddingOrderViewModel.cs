using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Domain.ViewModels.Wallets
{
    public class ClientSideWalletAddingOrderViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? OrderId { get; set; }
        public double Price { get; set; }
        public string IP { get; set; }
        public string OS { get; set; }
    }
    public enum ClientSideWalletAddingOrderResult
    {
        success
    }
}
