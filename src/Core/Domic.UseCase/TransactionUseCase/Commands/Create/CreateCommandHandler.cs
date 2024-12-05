﻿#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.UseCase.Attributes;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Account.Contracts.Interfaces;
using Domic.Domain.Account.Entities;
using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;
using Domic.Domain.Transaction.Enumerations;
using Domic.UseCase.TransactionUseCase.Contracts.Interfaces;
using Domic.UseCase.TransactionUseCase.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace Domic.UseCase.TransactionUseCase.Commands.Create;

public class CreateCommandHandler(
    ITransactionCommandRepository transactionCommandRepository, IGlobalUniqueIdGenerator globalUniqueIdGenerator,
    IDateTime dateTime, ISerializer serializer, IZarinPalBankGateway bankGateway,
    [FromKeyedServices("Http2")] IIdentityUser identityUser, IAccountCommandRepository accountCommandRepository
) : ICommandHandler<CreateCommand, string>
{
    private readonly object _validationResult;
    
    public Task BeforeHandleAsync(CreateCommand command, CancellationToken cancellationToken) 
        => Task.CompletedTask;

    [WithValidation]
    [WithTransaction]
    public async Task<string> HandleAsync(CreateCommand command, CancellationToken cancellationToken)
    {
        var targetAccount = _validationResult as Account;
        
        string gatewayUrl = string.Empty;
        
        var transaction = new Transaction(identityUser, serializer, globalUniqueIdGenerator, dateTime,
            command.AccountId, command.IncreasedAmount, command.DecreasedAmount, command.TransactionType
        );

        if (command.TransactionType == TransactionType.IncreasedAmount)
        {
            transaction.InActive(dateTime, identityUser, serializer);
            
            var requestDto = new ZarinPalRequestDto { Amount = command.IncreasedAmount.Value };

            var gatewayResponse = await bankGateway.RequestAsync(requestDto, cancellationToken);

            if (gatewayResponse.result)
            {
                transaction.Active(dateTime, identityUser, serializer);
                
                targetAccount.IncreaseBalance(dateTime, identityUser, serializer, command.IncreasedAmount.Value);
                
                gatewayUrl = gatewayResponse.url;
            }
        }
        
        await transactionCommandRepository.AddAsync(transaction, cancellationToken);
        await accountCommandRepository.ChangeAsync(targetAccount, cancellationToken);

        return gatewayUrl;
    }

    public Task AfterHandleAsync(CreateCommand command, CancellationToken cancellationToken)
        => Task.CompletedTask;
}