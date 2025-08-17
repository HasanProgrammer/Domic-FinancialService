using System.Linq.Expressions;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="countPerPage"></param>
    /// <param name="pageNumber"></param>
    /// <param name="order"></param>
    /// <param name="accending"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="projection"></param>
    /// <param name="conditions"></param>
    /// <typeparam name="TViewModel"></typeparam>
    /// <returns></returns>
    public Task<List<TViewModel>> FindAllWithPaginateAndOrderingByProjectionConditionallyAsync<TViewModel>(
        int countPerPage,
        int pageNumber,
        Order order,
        bool accending,
        CancellationToken cancellationToken,
        Expression<Func<Entities.Transaction, TViewModel>> projection,
        Expression<Func<Entities.Transaction, bool>> condition
    );
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="predict"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<long> CountRowsConditionallyAsync(Expression<Func<Entities.Transaction, bool>> predict,
        CancellationToken cancellationToken
    ) => throw new NotImplementedException();
}