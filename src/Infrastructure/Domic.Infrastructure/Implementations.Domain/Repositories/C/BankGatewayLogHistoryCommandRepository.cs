using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;
using Domic.Persistence.Contexts.C;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class BankGatewayLogHistoryCommandRepository(SQLContext context) : IBankGatewayLogHistoryCommandRepository
{
    public Task AddAsync(BankGatewayLogHistory entity, CancellationToken cancellationToken)
    {
        context.LogHistories.Add(entity);

        return Task.CompletedTask;
    }

    public Task AddRangeAsync(IEnumerable<BankGatewayLogHistory> entities, CancellationToken cancellationToken)
    {
        context.LogHistories.AddRange(entities);

        return Task.CompletedTask;
    }
}