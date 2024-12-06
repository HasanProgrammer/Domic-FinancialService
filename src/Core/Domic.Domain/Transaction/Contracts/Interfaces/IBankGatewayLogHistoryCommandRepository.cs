using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;

namespace Domic.Domain.Transaction.Contracts.Interfaces;

public interface IBankGatewayLogHistoryCommandRepository : ICommandRepository<BankGatewayLogHistory, string>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task AddRangeAsync(IEnumerable<BankGatewayLogHistory> entities, CancellationToken cancellationToken);
}