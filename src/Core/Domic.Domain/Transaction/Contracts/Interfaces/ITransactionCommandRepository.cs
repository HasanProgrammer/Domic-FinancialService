using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.Transaction.Contracts.Interfaces;

public interface ITransactionCommandRepository : ICommandRepository<Entities.Transaction, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<Entities.Transaction> FindBySecretConnectionKeyAsync(string key,
        CancellationToken cancellationToken
    );
}