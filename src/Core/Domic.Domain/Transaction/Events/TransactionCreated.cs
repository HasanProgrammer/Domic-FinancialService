using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;
using Domic.Domain.Transaction.Enumerations;

namespace Domic.Domain.Transaction.Events;

[EventConfig(ExchangeType = Exchange.FanOut, Exchange = "Financial_Transaction_Exchange")]
public class TransactionCreated : CreateDomainEvent<string>
{
    public required string AccountId { get; init; }
    public required long? IncreasedAmount { get; init; }
    public required long? DecreasedAmount { get; init; }
    public required TransactionType TransactionType { get; init; }
}