using Domic.Core.Common.ClassEnums;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.Account.Entities;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class UserCreatedEventBusHandler(IAccountCommandRepository accountCommandRepository, IDateTime dateTime,
    IGlobalUniqueIdGenerator globalUniqueIdGenerator
) : IConsumerEventBusHandler<UserCreated>
{
    public Task BeforeHandleAsync(UserCreated @event, CancellationToken cancellationToken) => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Command)]
    public Task HandleAsync(UserCreated @event, CancellationToken cancellationToken)
    {
        var newAccount = new Account(globalUniqueIdGenerator, dateTime,
            @event.Id, $"{@event.FirstName} {@event.LastName}", @event.CreatedBy, @event.CreatedRole, 0
        );
        
        return accountCommandRepository.AddAsync(newAccount, cancellationToken);
    }

    public Task AfterHandleAsync(UserCreated @event, CancellationToken cancellationToken) => Task.CompletedTask;
}