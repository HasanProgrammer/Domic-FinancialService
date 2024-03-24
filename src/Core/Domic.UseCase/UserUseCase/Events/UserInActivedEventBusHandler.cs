using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class UserInActivedEventBusHandler(IAccountCommandRepository accountCommandRepository, IDateTime dateTime) 
    : IConsumerEventBusHandler<UserActived>
{
    public void Handle(UserActived @event){}

    [WithTransaction]
    public async Task HandleAsync(UserActived @event, CancellationToken cancellationToken)
    {
        var account = await accountCommandRepository.FindByUserIdEagerLoadingAsync(@event.Id, cancellationToken);
        
        account.InActive(dateTime, @event.UpdatedBy, @event.UpdatedRole);
        
        foreach (var GiftTransaction in account.GiftTransactions)
            GiftTransaction.InActive(dateTime, @event.UpdatedBy, @event.UpdatedRole);
        
        accountCommandRepository.Change(account);
    }
}