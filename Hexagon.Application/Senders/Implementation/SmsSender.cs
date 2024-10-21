using Hexagon.Application.Senders.Interfaces;
using Kavenegar;
using Kavenegar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Senders.Implementation
{
    public class SmsSender : ISmsSender
    {
        private readonly KavenegarApi _KavenegarApi;
        public SmsSender()
        {
            _KavenegarApi = new KavenegarApi("");
        }
        public SendResult SendMessage(string PhoneNumber, string message)
        {
            return _KavenegarApi.Send("Sender",PhoneNumber,message);
        }
    }
}
