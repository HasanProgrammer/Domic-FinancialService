namespace Domic.Domain.GiftTransaction.Enumerations;

public enum TransactionType : byte
{
    IncreasedAmountFromDomicGift,
    DecreasedAmountFromDomicGift,
    IncreasedAmountFromDomicUser,
    DecreasedAmountFromDomicUser
}