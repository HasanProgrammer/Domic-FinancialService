#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TransactionUseCase.Commands.ChangeAmountTransactionRequest;

public class ChangeAmountTransactionRequestCommandHandler(
    ITransactionRequestCommandRepository transactionRequestCommandRepository, 
    [FromKeyedServices("Http2")] IIdentityUser identityUser, IDateTime dateTime, ISerializer serializer
) : ICommandHandler<ChangeAmountTransactionRequestCommand, bool>
{
    private readonly object _validationResult;
    
    public Task BeforeHandleAsync(ChangeAmountTransactionRequestCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [WithTransaction]
    [WithValidation]
    public async Task<bool> HandleAsync(ChangeAmountTransactionRequestCommand command, CancellationToken cancellationToken)
    {
        var targetRequest = _validationResult as Request;
        
        targetRequest.ChangeAmount(identityUser, dateTime, serializer, command.Amount * 10);

        await transactionRequestCommandRepository.ChangeAsync(targetRequest, cancellationToken);

        return true;
    }

    public Task AfterHandleAsync(ChangeAmountTransactionRequestCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}