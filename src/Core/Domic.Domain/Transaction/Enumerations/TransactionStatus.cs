namespace Domic.Domain.Transaction.Enumerations;

public enum TransactionStatus : byte
{
    Requested, Accepted, Rejected, Proccessing
}