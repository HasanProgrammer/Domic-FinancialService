using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Transaction.Enumerations;

namespace Domic.UseCase.TransactionUseCase.Commands.ChangeStatusTransactionRequest;

public class ChangeStatusTransactionRequestCommand : ICommand<bool>
{
    public string Id { get; set; }
    public TransactionStatus Status { get; set; }
    public string RejectReason { get; set; }
    public string BankTransferReceiptImage { get; set; }
}