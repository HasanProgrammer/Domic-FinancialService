#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.Account.Entities;
using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;
using Domic.Domain.Transaction.Enumerations;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.AccountUseCase.Commands.DecreaseBalance;

public class DecreaseBalanceCommandHandler(IAccountCommandRepository accountCommandRepository,
    ITransactionCommandRepository transactionCommandRepository, IDateTime dateTime, ISerializer serializer,
    [FromKeyedServices("Http2")] IIdentityUser identityUser, IGlobalUniqueIdGenerator globalUniqueIdGenerator
) : ICommandHandler<DecreaseBalanceCommand, bool>
{
    private readonly object _validationResult;
    
    public Task BeforeHandleAsync(DecreaseBalanceCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<bool> HandleAsync(DecreaseBalanceCommand command, CancellationToken cancellationToken)
    {
        var targetAccount = _validationResult as Account;
        
        targetAccount.DecreaseBalance(dateTime, identityUser, serializer, command.Value * 10);

        await accountCommandRepository.ChangeAsync(targetAccount, cancellationToken);

        var newTransaction = new Transaction(identityUser, serializer, globalUniqueIdGenerator, dateTime,
            command.AccountId, null, command.Value, TransactionType.DecreasedAmount
        );

        await transactionCommandRepository.AddAsync(newTransaction, cancellationToken);

        return true;
    }

    public Task AfterHandleAsync(DecreaseBalanceCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}