using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class TransactionRequestCommandRepository(SQLContext context) : ITransactionRequestCommandRepository
{
    public Task AddAsync(Request entity, CancellationToken cancellationToken)
    {
        context.Requests.Add(entity);

        return Task.CompletedTask;
    }

    public Task ChangeAsync(Request entity, CancellationToken cancellationToken)
    {
        context.Requests.Update(entity);

        return Task.CompletedTask;
    }

    public Task<Request> FindByIdAsync(object id, CancellationToken cancellationToken) 
        => context.Requests.AsNoTracking().FirstOrDefaultAsync(request => request.Id == id as string, cancellationToken);
}