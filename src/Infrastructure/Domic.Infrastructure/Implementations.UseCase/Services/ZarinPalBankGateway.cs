using Domic.UseCase.TransactionUseCase.Contracts.Interfaces;
using Domic.UseCase.TransactionUseCase.DTOs;
using Dto.Payment;
using Microsoft.Extensions.Hosting;
using ZarinPal.Class;

namespace Domic.Infrastructure.Implementations.UseCase.Services;

public class ZarinPalBankGateway(IHostEnvironment environment) : IZarinPalBankGateway
{
    public async Task<(bool result, string url, string secretKey)> RequestAsync(ZarinPalRequestDto dto, 
        CancellationToken cancellationToken
    )
    {
        var result = await new Payment().Request(
            new DtoRequest {
                Description = dto.Description,
                Amount      = (int)dto.Amount,
                CallbackUrl = Environment.GetEnvironmentVariable("ZarinPalCallbackUrl"),
                MerchantId  = Environment.GetEnvironmentVariable("ZarinPalMerchentId")
            }, 
            environment.IsDevelopment() ? Payment.Mode.sandbox : Payment.Mode.zarinpal
        );

        return (
            result.Status == 100 ,
            environment.IsDevelopment()
                ? $"{Environment.GetEnvironmentVariable("ZarinPalSandBoxUrl")}/{result.Authority}"
                : $"{Environment.GetEnvironmentVariable("ZarinPalUrl")}/{result.Authority}" ,
            result.Authority
        );
    }
    
    public async Task<(bool result, string transactionNumber)> VerificationAsync(ZarinPalVerificationDto dto,
        CancellationToken cancellationToken
    )
    {
        var result = await new Payment().Verification(new DtoVerification {
            Amount     = (int)dto.Amount,
            Authority  = dto.Authority,
            MerchantId = Environment.GetEnvironmentVariable("ZarinPalMerchentId")
        }, environment.IsDevelopment() ? Payment.Mode.sandbox : Payment.Mode.zarinpal);

        return ( result.Status == 100 , result.RefId.ToString() );
    }
}