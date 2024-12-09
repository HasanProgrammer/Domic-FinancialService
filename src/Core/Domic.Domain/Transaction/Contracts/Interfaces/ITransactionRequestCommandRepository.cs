using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.Transaction.Contracts.Interfaces;

public interface ITransactionRequestCommandRepository : ICommandRepository<Entities.Request, string>;