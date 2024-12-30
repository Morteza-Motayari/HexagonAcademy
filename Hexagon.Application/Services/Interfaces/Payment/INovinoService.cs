using Hexagon.Domain.DTOs.NovinoPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Application.Services.Interfaces.Payment
{
    public interface INovinoService
    {
        Task<NovinoGetPaymentUrlResponseDto> CreateRequestAsync(NovinoGetPaymentUrlRequestDto model);
        Task<NovinoVerifyPaymentResponseDto> Verifyasync(NovinoVerifyPaymentRequestDto model);
    }
}
