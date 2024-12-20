using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.Account.Contracts.Interfaces;

namespace Domic.UseCase.TransactionUseCase.Commands.CreateTransactionRequest;

public class CreateTransactionRequestCommandValidator(IAccountCommandRepository accountCommandRepository) 
    : IValidator<CreateTransactionRequestCommand>
{
    public async Task<object> ValidateAsync(CreateTransactionRequestCommand input, CancellationToken cancellationToken)
    {
        if (!await accountCommandRepository.IsExistByIdAsync(input.AccountId, cancellationToken))
            throw new UseCaseException(
                string.Format("حساب کیف پولی با شناسه {0} موجود نمی باشد!", input.AccountId)
            );

        return default;
    }
}