using Hexagon.Application.Senders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Senders.Implementation
{
    public class EmailSender : IEmailSender
    {
        public async Task<bool> Send(string to, string subject, string body)
        {
            //TODO adding email Service Sender
            await Task.CompletedTask;
            return true;
        }
    }
}
