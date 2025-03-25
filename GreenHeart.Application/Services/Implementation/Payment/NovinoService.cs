using GreenHeart.Application.Services.Interfaces.Payment;
using GreenHeart.Domain.DTOs.NovinoPay;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHeart.Application.Services.Implementation.Payment
{
    public class NovinoService(HttpClient httpClient) : INovinoService
    {
        public async Task<NovinoGetPaymentUrlResponseDto> CreateRequestAsync(NovinoGetPaymentUrlRequestDto model)
        {
            string body=JsonConvert.SerializeObject(model);

            HttpContent content=new StringContent(body,Encoding.UTF8,"application/json");

            var response = await httpClient.PostAsync("https://api.novinopay.com/payment/ipg/v2/request", content);

            var responseString=await response.Content.ReadAsStringAsync();

            var result=JsonConvert.DeserializeObject<NovinoGetPaymentUrlResponseDto>(responseString);

            return result;
        }

        public async Task<NovinoVerifyPaymentResponseDto> Verifyasync(NovinoVerifyPaymentRequestDto model)
        {
            string body = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.novinopay.com/payment/ipg/v2/verification", content);

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<NovinoVerifyPaymentResponseDto>(responseString);

            return result;
        }
    }
}
