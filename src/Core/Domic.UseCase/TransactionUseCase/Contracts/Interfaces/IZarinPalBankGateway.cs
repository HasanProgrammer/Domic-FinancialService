using Domic.UseCase.TransactionUseCase.DTOs;

namespace Domic.UseCase.TransactionUseCase.Contracts.Interfaces;

public interface IZarinPalBankGateway
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<(bool result, string url, string secretKey)> RequestAsync(ZarinPalRequestDto dto, 
        CancellationToken cancellationToken
    );
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<(bool result, string transactionNumber)> VerificationAsync(ZarinPalVerificationDto dto,
        CancellationToken cancellationToken
    );
}