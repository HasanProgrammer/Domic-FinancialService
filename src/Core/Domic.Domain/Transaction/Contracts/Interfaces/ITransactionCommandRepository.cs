using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.Transaction.Contracts.Interfaces;

public interface ITransactionCommandRepository : ICommandRepository<Entities.Transaction, string>;