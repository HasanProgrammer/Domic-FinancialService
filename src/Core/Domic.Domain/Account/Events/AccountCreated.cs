using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.Service.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Queue = "")]
public class AccountCreated : CreateDomainEvent<string>
{
    public required long Balance { get; init; }
}