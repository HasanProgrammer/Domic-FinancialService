using Domic.Core.UseCase.Contracts.Interfaces;

namespace Domic.UseCase.AccountUseCase.Commands.DecreaseBalance;

public class DecreaseBalanceCommand : ICommand<bool>
{
    public string AccountId { get; set; }
    public long Value { get; set; }
}