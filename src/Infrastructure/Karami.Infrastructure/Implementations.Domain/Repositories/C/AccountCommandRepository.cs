using Karami.Domain.Service.Contracts.Interfaces;
using Karami.Persistence.Contexts.C;

namespace Karami.Infrastructure.Implementations.Domain.Repositories.C;

public class AccountCommandRepository : IAccountCommandRepository
{
    private readonly SQLContext _sqlContext;

    public AccountCommandRepository(SQLContext sqlContext)
    {
        _sqlContext = sqlContext;
    }
}