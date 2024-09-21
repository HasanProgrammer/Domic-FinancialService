using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Enumerations;

namespace Domic.Domain.Account.Events;

[EventConfig(ExchangeType = Exchange.FanOut, Exchange = "Financial_Account_Exchange", Queue = "Financial_Account_Queue")]
public class AccountInActived : UpdateDomainEvent<string>;