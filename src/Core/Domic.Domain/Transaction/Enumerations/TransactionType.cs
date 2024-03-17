namespace Domic.Domain.Transaction.Enumerations;

public enum TransactionType : byte
{
    IncreasedAmountFromDomicGift,
    DecreasedAmountFromDomicGift,
    IncreasedAmountFromDomicUser,
    DecreasedAmountFromDomicUser
}