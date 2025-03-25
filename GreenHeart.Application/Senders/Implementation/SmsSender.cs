using GreenHeart.Application.Senders.Interfaces;
using GreenHeart.Application.Statics;
using Kavenegar;
using Kavenegar.Models;
using Microsoft.Extensions.Configuration;
using mpNuget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GreenHeart.Application.Senders.Implementation
{
    public class SmsSender : ISmsSender
    {
        public bool SendMessage(string PhoneNumber, string message)
        {
            const bool isFlash = false;
            const string from = "50002710090964";
            MelipayamakStatics melipayamakStatics = new MelipayamakStatics();            
            
            Uri apiBaseAddress = new Uri("https://console.melipayamak.com");
            using (HttpClient client = new HttpClient() { BaseAddress = apiBaseAddress })
            {
                //You may need to Install - Package Microsoft.AspNet.WebApi.Client

                //var result = client.PostAsJsonAsync("api/send/simple/5ba7de3f21f84a76be6376fa4b92914b",
                //    new { from = "50002710090964", to = PhoneNumber, text = message }).Result;
                // var response = result.Content.ReadAsStringAsync().Result;

                var result = client.PostAsJsonAsync("api/send/otp/5ba7de3f21f84a76be6376fa4b92914b",
        new { to = PhoneNumber }).Result;
                var response = result.Content.ReadAsStringAsync().Result;

                return true;
            }
            
        }
    }
}
