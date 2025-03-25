using Kavenegar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Senders.Interfaces
{
    public interface ISmsSender
    {
        bool SendMessage(string PhoneNumber,string message);
    }
}
