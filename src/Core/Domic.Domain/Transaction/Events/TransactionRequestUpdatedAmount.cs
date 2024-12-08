using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.Transaction.Events;

[EventConfig(ExchangeType = Exchange.FanOut, Exchange = "Financial_TransactionRequest_Exchange")]
public class TransactionRequestUpdatedAmount : UpdateDomainEvent<string>
{
    public required long? Amount { get; init; }
}