using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TransactionUseCase.Commands.PaymentVerification;

public class PaymentVerificationCommand : ICommand<PaymentVerificationCommandResponse>
{
    public long Amount { get; set; }
    public string BankGatewaySecretKey { get; set; }
}