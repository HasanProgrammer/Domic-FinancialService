using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Transaction.Enumerations;

namespace Domic.UseCase.TransactionUseCase.Commands.Create;

public class CreateTransactionCommand : ICommand<bool>
{
    public required string AccountId { get; init; }
    public required string TransactionId { get; init; }
    public required long? IncreasedAmount { get; init; }
    public required long? DecreasedAmount { get; init; }
    public required TransactionType TransactionType { get; init; }
}