using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.UseCase.TransactionUseCase.Contracts.Interfaces;
using Domic.UseCase.TransactionUseCase.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TransactionUseCase.Commands.PaymentVerification;

public class PaymentVerificationCommandHandler(
    IZarinPalBankGateway zarinPalBankGateway, 
    IBankGatewayLogHistoryCommandRepository bankGatewayLogHistoryCommandRepository,
    ITransactionCommandRepository transactionCommandRepository, IDateTime dateTime, ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser
) : ICommandHandler<PaymentVerificationCommand, bool>
{
    public Task BeforeHandleAsync(PaymentVerificationCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;

    public async Task<bool> HandleAsync(PaymentVerificationCommand command, CancellationToken cancellationToken)
    {
        var verifyDto = new ZarinPalVerificationDto {
            Amount = command.Amount,
            Authority = command.BankGatewaySecretKey
        };

        var result = await zarinPalBankGateway.VerificationAsync(verifyDto, cancellationToken);

        if (result)
        {
            var targetTransaction =
                await transactionCommandRepository.FindBySecretConnectionKeyAsync(command.BankGatewaySecretKey,
                    cancellationToken
                );
            
            targetTransaction.Active(dateTime, identityUser, serializer, true);
            
            //var newLogHistory = new BankGatewayLogHistory()

            await transactionCommandRepository.ChangeAsync(targetTransaction, cancellationToken);
        }

        return result;
    }

    public Task AfterHandleAsync(PaymentVerificationCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}