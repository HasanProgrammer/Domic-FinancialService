using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.GiftTransaction.Enumerations;

namespace Domic.UseCase.GiftTransactionUseCase.Commands.Create;

public class CreateGiftTransactionCommand : Auditable, ICommand<bool>
{
    public required string AccountId { get; init; }
    public required string GiftTransactionId { get; init; }
    public required long? IncreasedAmount { get; init; }
    public required long? DecreasedAmount { get; init; }
    public required TransactionType TransactionType { get; init; }
}