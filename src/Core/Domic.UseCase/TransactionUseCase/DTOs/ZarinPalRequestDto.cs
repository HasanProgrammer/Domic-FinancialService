namespace Domic.UseCase.TransactionUseCase.DTOs;

public class ZarinPalRequestDto
{
    public string Description { get; } = "افزایش اعتبار کیف پول";
    public long Amount { get; set; }
}