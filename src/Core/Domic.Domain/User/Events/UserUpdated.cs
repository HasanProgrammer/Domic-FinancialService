using Domic.Core.Domain.Attributes;
using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.User.Events;

[EventConfig(Queue = "Financial_User_Queue")]
public class UserUpdated : UpdateDomainEvent<string>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}