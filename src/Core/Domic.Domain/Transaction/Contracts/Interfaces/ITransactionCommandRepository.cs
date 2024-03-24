using Domic.Core.Domain.Contracts.Interfaces;

namespace Domic.Domain.GiftTransaction.Contracts.Interfaces;

public interface IGiftTransactionCommandRepository : ICommandRepository<GiftTransaction.Entities.GiftTransaction, string>;