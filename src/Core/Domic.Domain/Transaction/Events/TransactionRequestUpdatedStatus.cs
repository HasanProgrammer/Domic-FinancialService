using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;
using Domic.Domain.Transaction.Enumerations;

namespace Domic.Domain.Transaction.Events;

[EventConfig(ExchangeType = Exchange.FanOut, Exchange = "Financial_TransactionRequest_Exchange")]
public class TransactionRequestUpdatedStatus : UpdateDomainEvent<string>
{
    public required TransactionStatus Status { get; init; }
    public required string RejectReason { get; init; }
}