﻿using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;
using Domic.Domain.Transaction.Enumerations;

namespace Domic.Domain.Transaction.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = "Financial_Transaction_Exchange", Queue = "Financial_Transaction_Queue")]
public class TransactionCreated : CreateDomainEvent<string>
{
    public required string AccountId { get; init; }
    public required string TransactionId { get; init; }
    public required long? IncreasedAmount { get; init; }
    public required long? DecreasedAmount { get; init; }
    public required TransactionType TransactionType { get; init; }
}