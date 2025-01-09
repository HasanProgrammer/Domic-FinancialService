#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;
using Domic.Domain.Transaction.Enumerations;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TransactionUseCase.Commands.ChangeStatusTransactionRequest;

public class ChangeStatusTransactionRequestCommandHandler(
    ITransactionRequestCommandRepository transactionRequestCommandRepository, IAccountCommandRepository accountCommandRepository,
    ITransactionCommandRepository transactionCommandRepository, IGlobalUniqueIdGenerator globalUniqueIdGenerator,
    [FromKeyedServices("Http2")] IIdentityUser identityUser, IDateTime dateTime, ISerializer serializer
) : ICommandHandler<ChangeStatusTransactionRequestCommand, bool>
{
    private readonly object _validationResult;
    
    public Task BeforeHandleAsync(ChangeStatusTransactionRequestCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [WithTransaction]
    [WithValidation]
    public async Task<bool> HandleAsync(ChangeStatusTransactionRequestCommand command, CancellationToken cancellationToken)
    {
        var targetRequest = _validationResult as Request;
        
        targetRequest.ChangeStatus(identityUser, dateTime, serializer, command.Status, command.RejectReason, 
            command.BankTransferReceiptImage
        );

        await transactionRequestCommandRepository.ChangeAsync(targetRequest, cancellationToken);

        if (command.Status == TransactionStatus.Accepted)
        {
            targetRequest.Account.DecreaseBalance(dateTime, identityUser, serializer, targetRequest.Amount.Value.Value);

            await accountCommandRepository.ChangeAsync(targetRequest.Account, cancellationToken);

            var newTransaction = new Transaction(identityUser, serializer, globalUniqueIdGenerator, dateTime,
                targetRequest.AccountId, targetRequest.Amount.Value.Value, null, TransactionType.DecreasedAmount
            );

            await transactionCommandRepository.AddAsync(newTransaction, cancellationToken);
        }

        return true;
    }

    public Task AfterHandleAsync(ChangeStatusTransactionRequestCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}