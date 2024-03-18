﻿using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class UserCreatedEventBusHandler(IAccountCommandRepository accountCommandRepository, IDateTime dateTime) 
    : IConsumerEventBusHandler<UserActived>
{
    public void Handle(UserActived @event){}

    [WithTransaction]
    public async Task HandleAsync(UserActived @event, CancellationToken cancellationToken)
    {
        var account = await accountCommandRepository.FindByUserIdEagerLoadingAsync(@event.Id, cancellationToken);
        
        account.Active(dateTime, @event.UpdatedBy, @event.UpdatedRole);

        foreach (var transaction in account.Transactions)
            transaction.Active(dateTime, @event.UpdatedBy, @event.UpdatedRole);
        
        accountCommandRepository.Change(account);
    }
}