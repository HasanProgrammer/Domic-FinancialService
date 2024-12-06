using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.Transaction.Events;

[EventConfig(ExchangeType = Exchange.FanOut, Exchange = "Financial_LogHistory_Exchange")]
public class BankGatewayLogHistoryStatusChanged : UpdateDomainEvent<string>
{
    public required string TransactionId { get; init; }
    public required int Status { get; init; }
}