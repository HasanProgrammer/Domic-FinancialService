using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TransactionUseCase.Commands.ChangeAmountTransactionRequest;

public class ChangeAmountTransactionRequestCommand : ICommand<bool>
{
    public string Id { get; set; }
    public long? Amount { get; set; }
}