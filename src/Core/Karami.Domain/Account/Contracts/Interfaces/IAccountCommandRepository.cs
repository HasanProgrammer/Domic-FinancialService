using Karami.Core.Domain.Contracts.Interfaces;

namespace Karami.Domain.Service.Contracts.Interfaces;

public interface IAccountCommandRepository : ICommandRepository<Entities.Account, string>
{
    
}