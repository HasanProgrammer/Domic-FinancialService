using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Account.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.AccountUseCase.Queries.CurrentBalence;

public class CurrentBalenceQueryHandler(
    IAccountCommandRepository accountCommandRepository,
    [FromKeyedServices("Http2")] IIdentityUser identityUser
) : IQueryHandler<CurrentBalenceQuery, long>
{
    public async Task<long> HandleAsync(CurrentBalenceQuery query, CancellationToken cancellationToken)
        => ( await accountCommandRepository.CurrentBalenceAsync(identityUser.GetIdentity(), cancellationToken) ) / 10;
}