using GreenHeart.Domain.Enums.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Orders.ClassesOrder
{
    public class CreateClassOrderViewModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public int Price { get; set; }
        public UserGender Gender { get; set; }
        public string? ClassSlug { get; set; }
        public bool IsRegisterted { get; set; }
    }
    public enum CreateClassOrderResult
    {
        Success,
        ClassAlreadyRegistered,
        InCorrectGender
    }
}
