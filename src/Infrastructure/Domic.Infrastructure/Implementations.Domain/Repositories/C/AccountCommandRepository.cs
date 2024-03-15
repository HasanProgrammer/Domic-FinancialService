using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.Account.Entities;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class AccountCommandRepository(SQLContext context) : IAccountCommandRepository
{
    public Task<Account> FindByUserIdAsync(string userId, CancellationToken cancellationToken) 
        => context.Accounts.FirstOrDefaultAsync(account => account.UserId == userId, cancellationToken);

    public void Change(Account entity) => context.Accounts.Update(entity);
}