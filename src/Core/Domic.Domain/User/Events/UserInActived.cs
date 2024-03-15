using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Constants;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.Account.Events;

[MessageBroker(Queue = Broker.User_User_Queue)]
public class UserInActived : UpdateDomainEvent<string>;