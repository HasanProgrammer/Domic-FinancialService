using System.Net.Http.Json;
using System.Text;
using Domic.Core.Infrastructure.Extensions;
using Domic.Infrastructure.Implementations.UseCase.Services.DTOs;
using Domic.UseCase.TransactionUseCase.Contracts.Interfaces;
using Domic.UseCase.TransactionUseCase.DTOs;

namespace Domic.Infrastructure.Implementations.UseCase.Services;

public class ZarinPalBankGateway : IZarinPalBankGateway
{
    public async Task<(bool result, string url, string secretKey)> RequestAsync(ZarinPalRequestDto dto, 
        CancellationToken cancellationToken
    )
    {
        var zarinUrl          = Environment.GetEnvironmentVariable("ZarinPalUrl");
        var zarinGatewayUrl   = Environment.GetEnvironmentVariable("ZarinPalGatewayUrl");
        var zarinCallbackUrl  = Environment.GetEnvironmentVariable("ZarinPalCallbackUrl");
        var zarinMerchantCode = Environment.GetEnvironmentVariable("ZarinPalMerchentId");

        using var httpClient = new HttpClient();

        var requestDto = new {
            merchant_id = zarinMerchantCode,
            callback_url = zarinCallbackUrl,
            amount = dto.Amount,
            description = dto.Description
        };

        var response = await httpClient.PostAsync(
            zarinUrl,
            new StringContent(requestDto.Serialize(), Encoding.UTF8, "application/json"),
            cancellationToken
        );
            
        var result = await response.Content.ReadFromJsonAsync<ZarinPalResponseDto>(cancellationToken);

        return (
            result.data.code == 100 ,
            $"{zarinGatewayUrl}/{result.data.authority}?Amount={dto.Amount}",
            result.data.authority
        );
    }
    
    public async Task<(bool result, string transactionNumber)> VerificationAsync(ZarinPalVerificationDto dto,
        CancellationToken cancellationToken
    )
    {
        var zarinVerificationUrl = Environment.GetEnvironmentVariable("ZarinPalVerificationUrl");
        var zarinMerchantCode = Environment.GetEnvironmentVariable("ZarinPalMerchentId");
        
        using var httpClient = new HttpClient();

        var requestDto = new {
            merchant_id = zarinMerchantCode,
            amount = dto.Amount,
            authority = dto.Authority
        };

        var response = await httpClient.PostAsync(
            zarinVerificationUrl,
            new StringContent(requestDto.Serialize(), Encoding.UTF8, "application/json"),
            cancellationToken
        );
            
        var result = await response.Content.ReadFromJsonAsync<ZarinPalVerificationResponseDto>(cancellationToken);

        return ( result.data.code == 100 , result.data.ref_id.ToString() );
    }
}