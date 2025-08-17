using System.Linq.Expressions;
using Domic.Core.Domain.Enumerations;
using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class TransactionCommandRepository(SQLContext context) : ITransactionCommandRepository
{
    public Task<Transaction> FindBySecretConnectionKeyAsync(string key, 
        CancellationToken cancellationToken
    ) => context.Transactions.AsNoTracking()
                             .Include(transaction => transaction.Account)
                             .FirstOrDefaultAsync(
                                 transaction => transaction.LogHistories.Any(lg => lg.SecretConnectionKey == key),
                                 cancellationToken
                             );

    public Task<List<TViewModel>> FindAllWithPaginateAndOrderingByProjectionConditionallyAsync<TViewModel>(
        int countPerPage, int pageNumber, Order order, bool accending, CancellationToken cancellationToken,
        Expression<Func<Transaction, TViewModel>> projection, Expression<Func<Transaction, bool>> condition
    )
    {
        var query = context.Transactions.AsNoTracking().Where(condition);

        if (order == Order.Date)
            query = accending
                ? query.OrderBy(q => q.CreatedAt.EnglishDate)
                : query.OrderByDescending(q => q.CreatedAt.EnglishDate);
        else
            query = accending ? query.OrderBy(q => q.Id) : query.OrderByDescending(q => q.Id);
                
        return query.Skip((pageNumber - 1)*countPerPage)
                    .Take(countPerPage)
                    .Select(projection)
                    .ToListAsync(cancellationToken);
    }

    public async Task<long> CountRowsConditionallyAsync(Expression<Func<Transaction, bool>> predict,
        CancellationToken cancellationToken
    ) 
    {
        var count = await context.Transactions.AsNoTracking().Where(predict).CountAsync(cancellationToken);

        return count;
    }

    public Task ChangeAsync(Transaction entity, CancellationToken cancellationToken)
    {
        context.Transactions.Update(entity);

        return Task.CompletedTask;
    }

    public Task AddAsync(Transaction entity, CancellationToken cancellationToken)
    {
        context.Transactions.Add(entity);

        return Task.CompletedTask;
    }
}