using Domic.Core.Common.ClassEnums;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.User.Events;

namespace Domic.UseCase.UserUseCase.Events;

public class UserUpdatedEventBusHandler(IAccountCommandRepository accountCommandRepository, IDateTime dateTime
) : IConsumerEventBusHandler<UserUpdated>
{
    public Task BeforeHandleAsync(UserUpdated @event, CancellationToken cancellationToken) => Task.CompletedTask;

    [TransactionConfig(Type = TransactionType.Command)]
    public async Task HandleAsync(UserUpdated @event, CancellationToken cancellationToken)
    {
        var targetAcc = await accountCommandRepository.FindByUserIdAsync(@event.Id, cancellationToken);
        
        targetAcc.Change(dateTime, $"{@event.FirstName} {@event.LastName}", @event.UpdatedBy, @event.UpdatedRole);
        
        await accountCommandRepository.ChangeAsync(targetAcc, cancellationToken);
    }

    public Task AfterHandleAsync(UserUpdated @event, CancellationToken cancellationToken) => Task.CompletedTask;
}