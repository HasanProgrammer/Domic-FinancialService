using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.GiftTransaction.Contracts.Interfaces;
using Domic.Domain.GiftTransaction.Entities;

namespace Domic.UseCase.GiftTransactionUseCase.Commands.Create;

public class CreateGiftTransactionCommandHandler(
    IGiftTransactionCommandRepository GiftTransactionCommandRepository, IGlobalUniqueIdGenerator globalUniqueIdGenerator,
    IDateTime dateTime, ISerializer serializer
) : ICommandHandler<CreateGiftTransactionCommand, bool>
{
    [WithTransaction]
    public Task<bool> HandleAsync(CreateGiftTransactionCommand command, CancellationToken cancellationToken)
    {
        var newGiftTransaction = new GiftTransaction(globalUniqueIdGenerator, dateTime, command.AccountId,
            command.GiftTransactionId, command.IncreasedAmount, command.DecreasedAmount, command.TransactionType,
            command.UserId, serializer.Serialize(command.UserRoles)
        );
        
        GiftTransactionCommandRepository.Add(newGiftTransaction);

        return Task.FromResult(true);
    }
}