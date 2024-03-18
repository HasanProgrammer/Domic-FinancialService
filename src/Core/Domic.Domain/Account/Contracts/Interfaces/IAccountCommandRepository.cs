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
    public Task<Entities.Account> FindByUserIdAsync(string userId, CancellationToken cancellationToken)
        => throw new NotImplementedException();
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Entities.Account> FindByUserIdEagerLoadingAsync(string userId, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}