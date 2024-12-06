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