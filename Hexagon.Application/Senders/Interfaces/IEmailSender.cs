using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Senders.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> Send(string to,string subject, string body);
    }
}
