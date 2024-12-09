using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Transaction.Enumerations;

namespace Domic.UseCase.TransactionUseCase.Commands.CreateTransactionRequest;

public class CreateTransactionRequestCommand : ICommand<bool>
{
    public string AccountId { get; set; }
    public long Amount { get; set; }
    public TransactionStatus Status { get; set; }
    public string RejectReason { get; set; }
    public string BankTransferReceiptImage { get; set; }
}