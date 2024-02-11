using Domic.Domain.Service.Contracts.Interfaces;
using Domic.Persistence.Contexts.C;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class AccountCommandRepository : IAccountCommandRepository
{
    private readonly SQLContext _sqlContext;

    public AccountCommandRepository(SQLContext sqlContext)
    {
        _sqlContext = sqlContext;
    }
}