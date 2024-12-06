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
                             .FirstOrDefaultAsync(
                                 transaction => transaction.LogHistories.Any(lg => lg.SecretConnectionKey == key),
                                 cancellationToken
                             );
                              
}