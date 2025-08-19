using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.Account.Entities;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class AccountCommandRepository(SQLContext context) : IAccountCommandRepository
{
    public Task<long> CurrentBalenceAsync(string userId, CancellationToken cancellationToken) 
        => context.Accounts.AsNoTracking()
                           .Where(acc => acc.UserId == userId)
                           .Select(acc => acc.Balance.Value.Value)
                           .FirstOrDefaultAsync(cancellationToken);

    public Task<Account> FindByIdAsync(object id, CancellationToken cancellationToken)
        => context.Accounts.AsNoTracking().FirstOrDefaultAsync(account => account.Id == id as string, cancellationToken);

    public Task<Account> FindByUserIdAsync(string userId, CancellationToken cancellationToken) 
        => context.Accounts.AsNoTracking().FirstOrDefaultAsync(account => account.UserId == userId, cancellationToken);
    
    public Task<Account> FindByUserIdEagerLoadingAsync(string userId, CancellationToken cancellationToken) 
        => context.Accounts
                  .Include(account => account.Transactions)
                  .FirstOrDefaultAsync(account => account.UserId == userId, cancellationToken);

    public Task<bool> IsExistByIdAsync(string id, CancellationToken cancellationToken)
        => context.Accounts.AnyAsync(acc => acc.Id == id, cancellationToken);

    public Task ChangeAsync(Account entity, CancellationToken cancellationToken)
    {
        context.Accounts.Update(entity);

        return Task.CompletedTask;
    }

    public Task AddAsync(Account entity, CancellationToken cancellationToken)
    {
        context.Accounts.Add(entity);

        return Task.CompletedTask;
    }
}