using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;
using Domic.Domain.Transaction.Enumerations;
using Domic.UseCase.TransactionUseCase.Contracts.Interfaces;
using Domic.UseCase.TransactionUseCase.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TransactionUseCase.Commands.PaymentVerification;

public class PaymentVerificationCommandHandler(
    IZarinPalBankGateway zarinPalBankGateway, 
    IBankGatewayLogHistoryCommandRepository bankGatewayLogHistoryCommandRepository,
    ITransactionCommandRepository transactionCommandRepository, IDateTime dateTime, ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser, IGlobalUniqueIdGenerator globalUniqueIdGenerator,
    IAccountCommandRepository accountCommandRepository
) : ICommandHandler<PaymentVerificationCommand, PaymentVerificationCommandResponse>
{
    public Task BeforeHandleAsync(PaymentVerificationCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [WithTransaction]
    public async Task<PaymentVerificationCommandResponse> HandleAsync(PaymentVerificationCommand command, CancellationToken cancellationToken)
    {
        var targetTransaction =
            await transactionCommandRepository.FindBySecretConnectionKeyAsync(command.BankGatewaySecretKey,
                cancellationToken
            );

        if (targetTransaction is not null && targetTransaction.IsActive == IsActive.InActive)
        {
            var verifyDto = new ZarinPalVerificationDto {
                Amount = command.Amount,
                Authority = command.BankGatewaySecretKey
            };

            var response = await zarinPalBankGateway.VerificationAsync(verifyDto, cancellationToken);
            
            if (response.result)
            {
                targetTransaction.Active(dateTime, identityUser, serializer, true);
            
                targetTransaction.Account.IncreaseBalance(dateTime, identityUser, serializer, command.Amount);

                await accountCommandRepository.ChangeAsync(targetTransaction.Account, cancellationToken);
                await transactionCommandRepository.ChangeAsync(targetTransaction, cancellationToken);
            }

            var newLogHistory = new BankGatewayLogHistory(
                identityUser, dateTime, globalUniqueIdGenerator, serializer, targetTransaction.Id,
                BankGatewayType.ZarinPal, 
                response.result ? BankGatewayStatus.SuccessVerificationPurchase : BankGatewayStatus.ErrorVerificationPurchase, 
                command.BankGatewaySecretKey, response.result ? response.transactionNumber : string.Empty
            );

            await bankGatewayLogHistoryCommandRepository.AddAsync(newLogHistory, cancellationToken);
        
            return new() { Status = response.result , TransactionNumber = response.transactionNumber };
        }

        return new() { Status = false , TransactionNumber = string.Empty };
    }

    public Task AfterHandleAsync(PaymentVerificationCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}