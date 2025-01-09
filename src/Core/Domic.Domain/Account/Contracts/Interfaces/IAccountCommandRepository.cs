using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.Account.Contracts.Interfaces;

public interface IAccountCommandRepository : ICommandRepository<Entities.Account, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Entities.Account> FindByUserIdEagerLoadingAsync(string userId, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<bool> IsExistByIdAsync(string id, CancellationToken cancellationToken);
}