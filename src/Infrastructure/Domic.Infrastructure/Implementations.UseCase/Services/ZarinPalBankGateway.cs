using System.Net.Http.Json;
using System.Text;
using Domic.Core.Infrastructure.Extensions;
using Domic.Infrastructure.Implementations.UseCase.Services.DTOs;
using Domic.UseCase.TransactionUseCase.Contracts.Interfaces;
using Domic.UseCase.TransactionUseCase.DTOs;
using Microsoft.Extensions.Hosting;

namespace Domic.Infrastructure.Implementations.UseCase.Services;

public class ZarinPalBankGateway(IHostEnvironment environment) : IZarinPalBankGateway
{
    public async Task<(bool result, string url, string secretKey)> RequestAsync(ZarinPalRequestDto dto, 
        CancellationToken cancellationToken
    )
    {
        var zarinCallbackUrl  = Environment.GetEnvironmentVariable("ZarinPalCallbackUrl");
        var zarinUrl          = Environment.GetEnvironmentVariable("ZarinPalUrl");
        var zarinSandBoxUrl   = Environment.GetEnvironmentVariable("ZarinPalSandBoxUrl");
        var zarinMerchantCode = Environment.GetEnvironmentVariable("ZarinPalMerchentId");

        using var httpClient = new HttpClient();

        var requestDto = new {
            merchant_id = zarinMerchantCode,
            callback_url = zarinCallbackUrl,
            amount = dto.Amount,
            description = dto.Description
        };

        var response = await httpClient.PostAsync(
            environment.IsDevelopment() ? $"https://sandbox.zarinpal.com/pg/v4/payment/request.json" : $"https://payment.zarinpal.com/pg/v4/payment/request.json",
            new StringContent(requestDto.Serialize(), Encoding.UTF8, "application/json"),
            cancellationToken
        );
            
        var result = await response.Content.ReadFromJsonAsync<ZarinPalResponseDto>(cancellationToken);

        return (
            result.data.code == 100 ,
            environment.IsDevelopment() 
                ? $"{zarinSandBoxUrl}/{result.data.authority}?Amount={dto.Amount}"
                : $"{zarinUrl}/{result.data.authority}?Amount={dto.Amount}",
            result.data.authority
        );
    }
    
    public async Task<(bool result, string transactionNumber)> VerificationAsync(ZarinPalVerificationDto dto,
        CancellationToken cancellationToken
    )
    {
        var zarinMerchantCode = Environment.GetEnvironmentVariable("ZarinPalMerchentId");
        
        using var httpClient = new HttpClient();

        var requestDto = new {
            merchant_id = zarinMerchantCode,
            amount = dto.Amount,
            authority = dto.Authority
        };

        var response = await httpClient.PostAsync(
            environment.IsDevelopment() ? $"https://sandbox.zarinpal.com/pg/v4/payment/verify.json" : $"https://payment.zarinpal.com/pg/v4/payment/verify.json",
            new StringContent(requestDto.Serialize(), Encoding.UTF8, "application/json"),
            cancellationToken
        );
            
        var result = await response.Content.ReadFromJsonAsync<ZarinPalVerificationResponseDto>(cancellationToken);

        return ( result.data.code == 101 , result.data.ref_id.ToString() );
    }
}