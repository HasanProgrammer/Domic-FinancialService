using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Core.Domain.Enumerations;

namespace Karami.Domain.Service.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = "", Queue = "")]
public class AccountActived : UpdateDomainEvent
{
    public string Id        { get; init; }
    public string UpdatedBy { get; init; }
    public bool IsActive    { get; init; }
}