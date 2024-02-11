using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.Service.Contracts.Interfaces;

public interface IAccountCommandRepository : ICommandRepository<Entities.Account, string>
{
    
}