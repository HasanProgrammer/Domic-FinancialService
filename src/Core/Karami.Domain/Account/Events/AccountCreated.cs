using Karami.Core.Domain.Attributes;
using Karami.Core.Domain.Contracts.Abstracts;
using Karami.Core.Domain.Enumerations;

namespace Karami.Domain.Service.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Queue = "")]
public class AccountCreated : CreateDomainEvent
{
    
}