using Domic.Core.Common.ClassConsts;
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
    public void Handle(UserCreated @event){}

    [TransactionConfig(Type = TransactionType.Command)]
    public Task HandleAsync(UserCreated @event, CancellationToken cancellationToken)
    {
        var newAccount = new Account(globalUniqueIdGenerator, dateTime, @event.Id, @event.CreatedBy,
            @event.CreatedRole, 0
        );
        
        accountCommandRepository.Add(newAccount);
        
        return Task.CompletedTask;
    }
}