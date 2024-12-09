using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Core.UseCase.Exceptions;
using Domic.Domain.Transaction.Contracts.Interfaces;

namespace Domic.UseCase.TransactionUseCase.Commands.ChangeStatusTransactionRequest;

public class ChangeStatusTransactionRequestCommandValidator(
    ITransactionRequestCommandRepository transactionRequestCommandRepository
) : IValidator<ChangeStatusTransactionRequestCommand>
{
    public async Task<object> ValidateAsync(ChangeStatusTransactionRequestCommand input, CancellationToken cancellationToken)
    {
        var targetRequest = await transactionRequestCommandRepository.FindByIdAsync(input.Id, cancellationToken);

        if (targetRequest is null)
            throw new UseCaseException(string.Format("درخواست واریز وجهی با شناسه {0} موجود نمی باشد!", input.Id));

        return targetRequest;
    }
}