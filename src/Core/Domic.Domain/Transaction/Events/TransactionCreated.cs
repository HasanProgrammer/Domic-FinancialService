using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;
using Domic.Domain.GiftTransaction.Enumerations;

namespace Domic.Domain.GiftTransaction.Events;

[EventConfig(ExchangeType = Exchange.FanOut, Exchange = "Financial_GiftTransaction_Exchange", Queue = "Financial_GiftTransaction_Queue")]
public class GiftTransactionCreated : CreateDomainEvent<string>
{
    public required string AccountId { get; init; }
    public required string GiftTransactionId { get; init; }
    public required long? IncreasedAmount { get; init; }
    public required long? DecreasedAmount { get; init; }
    public required TransactionType TransactionType { get; init; }
}