using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;

namespace Domic.UseCase.TransactionUseCase.Commands.Create;

public class CreateTransactionCommandHandler(
    ITransactionCommandRepository transactionCommandRepository, IGlobalUniqueIdGenerator globalUniqueIdGenerator,
    IDateTime dateTime, ISerializer serializer
) : ICommandHandler<CreateTransactionCommand, bool>
{
    [WithTransaction]
    public Task<bool> HandleAsync(CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        var newTransaction = new Transaction(globalUniqueIdGenerator, dateTime, command.AccountId,
            command.TransactionId, command.IncreasedAmount, command.DecreasedAmount, command.TransactionType,
            command.UserId, serializer.Serialize(command.UserRoles)
        );
        
        transactionCommandRepository.Add(newTransaction);

        return Task.FromResult(true);
    }
}