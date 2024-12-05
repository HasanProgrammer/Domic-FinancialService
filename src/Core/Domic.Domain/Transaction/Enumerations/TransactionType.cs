namespace Domic.Domain.Transaction.Enumerations;

public enum TransactionType : byte
{
    IncreasedAmountWalletFromGift,
    DecreasedAmountWalletFromGift,
    IncreasedAmountWalletFromStudent,
    DecreasedAmountWalletFromStudent,
    IncreasedAmountWalletFromTeacher,
    DecreasedAmountWalletFromTeacher
}