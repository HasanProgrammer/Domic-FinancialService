using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TransactionUseCase.Commands.CreateTransactionRequest;

public class CreateTransactionRequestCommandHandler(
    ITransactionRequestCommandRepository transactionRequestCommandRepository, IGlobalUniqueIdGenerator globalUniqueIdGenerator,
    [FromKeyedServices("Http2")] IIdentityUser identityUser, IDateTime dateTime, ISerializer serializer
) : ICommandHandler<CreateTransactionRequestCommand, bool>
{
    public Task BeforeHandleAsync(CreateTransactionRequestCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;

    [WithTransaction]
    public async Task<bool> HandleAsync(CreateTransactionRequestCommand command, CancellationToken cancellationToken)
    {
        var newTransactionRequest = new Request(globalUniqueIdGenerator, identityUser, dateTime, serializer, 
            command.AccountId, command.Amount
        );

        await transactionRequestCommandRepository.AddAsync(newTransactionRequest, cancellationToken);

        return true;
    }

    public Task AfterHandleAsync(CreateTransactionRequestCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}