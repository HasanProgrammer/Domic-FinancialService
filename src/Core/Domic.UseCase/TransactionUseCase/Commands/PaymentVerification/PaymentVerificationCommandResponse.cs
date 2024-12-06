namespace Domic.UseCase.TransactionUseCase.Commands.PaymentVerification;

public class PaymentVerificationCommandResponse
{
    public bool Status { get; set; }
    public string TransactionNumber { get; set; }
}