using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Domain.ViewModels.Users.Users
{
    public class AdminChagePasswordViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Password { get; set; }
    }
    public enum AdminChagePasswordResult
    {
        Success,
        UserNotFound
    }
}
