using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.Account.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class UserInActivedEventBusHandler(IAccountCommandRepository accountCommandRepository, IDateTime dateTime) 
    : IConsumerEventBusHandler<UserActived>
{
    public void Handle(UserActived @event){}

    [WithTransaction]
    public async Task HandleAsync(UserActived @event, CancellationToken cancellationToken)
    {
        var account = await accountCommandRepository.FindByUserIdAsync(@event.Id, cancellationToken);
        
        account.Active(dateTime, @event.UpdatedBy, @event.UpdatedRole);
        
        accountCommandRepository.Change(account);
    }
}