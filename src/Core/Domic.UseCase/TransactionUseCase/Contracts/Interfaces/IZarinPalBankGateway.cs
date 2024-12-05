using Domic.UseCase.TransactionUseCase.DTOs;

namespace Domic.UseCase.TransactionUseCase.Contracts.Interfaces;

public interface IZarinPalBankGateway
{
    public Task<(bool result, string url)> RequestAsync(ZarinPalRequestDto dto, CancellationToken cancellationToken);
    public Task<bool> VerificationAsync(ZarinPalVerificationDto dto, CancellationToken cancellationToken);
}