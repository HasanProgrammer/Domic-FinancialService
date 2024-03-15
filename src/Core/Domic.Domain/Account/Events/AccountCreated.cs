using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.Account.Events;

[MessageBroker(ExchangeType = Exchange.FanOut, Exchange = "Financial_Account_Exchange", Queue = "Financial_Account_Queue")]
public class AccountCreated : CreateDomainEvent<string>
{
    public required string UserId { get; init; }
    public required long Balance { get; init; }
}