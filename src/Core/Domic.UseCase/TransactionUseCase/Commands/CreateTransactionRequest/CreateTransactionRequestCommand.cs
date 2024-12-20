using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.TransactionUseCase.Commands.CreateTransactionRequest;

public class CreateTransactionRequestCommand : ICommand<bool>
{
    public string AccountId { get; set; }
    public long Amount { get; set; }
}