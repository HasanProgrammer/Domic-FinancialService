using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.Service.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = "", Queue = "")]
public class AccountActived : UpdateDomainEvent<string>
{
    public required bool IsActive { get; init; }
}