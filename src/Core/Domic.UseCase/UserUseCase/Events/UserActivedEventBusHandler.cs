using Domic.Core.Common.ClassConsts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class UserActivedEventBusHandler(IAccountCommandRepository accountCommandRepository, IDateTime dateTime) 
    : IConsumerEventBusHandler<UserActived>
{
    public Task BeforeHandleAsync(UserActived @event, CancellationToken cancellationToken) => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Command)]
    public async Task HandleAsync(UserActived @event, CancellationToken cancellationToken)
    {
        var account = await accountCommandRepository.FindByUserIdEagerLoadingAsync(@event.Id, cancellationToken);
        
        account.Active(dateTime, @event.UpdatedBy, @event.UpdatedRole);

        foreach (var transaction in account.Transactions)
            transaction.Active(dateTime, @event.UpdatedBy, @event.UpdatedRole);
        
        await accountCommandRepository.ChangeAsync(account, cancellationToken);
    }

    public Task AfterHandleAsync(UserActived @event, CancellationToken cancellationToken) => Task.CompletedTask;
}