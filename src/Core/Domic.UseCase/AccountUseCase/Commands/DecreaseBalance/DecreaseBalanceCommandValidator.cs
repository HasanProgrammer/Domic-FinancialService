using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.Account.Contracts.Interfaces;

namespace Domic.UseCase.AccountUseCase.Commands.DecreaseBalance;

public class DecreaseBalanceCommandValidator(IAccountCommandRepository accountCommandRepository) 
    : IValidator<DecreaseBalanceCommand>
{
    public async Task<object> ValidateAsync(DecreaseBalanceCommand input, CancellationToken cancellationToken)
    {
        var targetAccount = await accountCommandRepository.FindByIdAsync(input.AccountId, cancellationToken);

        if (targetAccount is null)
            throw new UseCaseException(string.Format("حساب کیف پولی با شناسه {0} موجود نمی باشد!", input.AccountId));

        return targetAccount;
    }
}