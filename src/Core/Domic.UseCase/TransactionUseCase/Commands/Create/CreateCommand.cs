using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Transaction.Enumerations;

namespace Domic.UseCase.TransactionUseCase.Commands.Create;

public class CreateCommand : ICommand<string>
{
    public List<string> Items { get; set; } //list of identity
    public required string AccountId { get; init; }
    public required long? IncreasedAmount { get; init; }
    public required long? DecreasedAmount { get; init; }
    public required TransactionType TransactionType { get; init; }
}