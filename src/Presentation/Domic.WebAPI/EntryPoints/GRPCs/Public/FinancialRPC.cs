using Domic.Core.Common.ClassExtensions;
using Domic.Core.Financial.Grpc;
using Domic.Core.Infrastructure.Extensions;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Commons.Enumerations;
using Domic.Domain.Transaction.Enumerations;
using Domic.UseCase.AccountUseCase.Commands.DecreaseBalance;
using Domic.UseCase.AccountUseCase.Queries.CurrentBalence;
using Domic.UseCase.TransactionUseCase.Commands.ChangeAmountTransactionRequest;
using Domic.UseCase.TransactionUseCase.Commands.ChangeStatusTransactionRequest;
using Domic.UseCase.TransactionUseCase.Commands.Create;
using Domic.UseCase.TransactionUseCase.Commands.CreateTransactionRequest;
using Domic.UseCase.TransactionUseCase.Commands.PaymentVerification;
using Domic.UseCase.TransactionUseCase.Queries.ReadAllTransactionPaginated;
using Grpc.Core;

using String = Domic.Core.Financial.Grpc.String;

namespace Domic.WebAPI.EntryPoints.GRPCs.Public;

public class FinancialRPC(IMediator mediator, IConfiguration configuration) : FinancialService.FinancialServiceBase
{
    public override async Task<CurrentBalenceResponse> CurrentBalence(CurrentBalenceRequest request, ServerCallContext context)
    {
        var result = await mediator.DispatchAsync(new CurrentBalenceQuery(), context.CancellationToken);

        return new() {
            Code = configuration.GetSuccessStatusCode(),
            Message = configuration.GetSuccessFetchDataMessage(),
            Body = new CurrentBalenceResponseBody { Amount = result }
        };
    }

    public override async Task<ReadAllTransactionResponse> ReadAllTransactionPaginated(ReadAllTransactionRequest request, ServerCallContext context)
    {
        var query = new ReadAllTransactionPaginatedQuery {
            PageNumber = request.PageNumber.Value,
            CountPerPage = request.CountPerPage.Value,
            UserId = request.UserId?.Value,
            Sort = (Sort)request.Sort.Value,
            SearchText = request.SearchText?.Value
        };
        
        var result = await mediator.DispatchAsync(query, context.CancellationToken);

        return new() {
            Code = configuration.GetSuccessStatusCode(),
            Message = configuration.GetSuccessFetchDataMessage(),
            Body = new ReadAllTransactionResponseBody { Transactions = result.Serialize() }
        };
    }

    public override Task<ReadAllTransactionRequestPaginatedResponse> ReadAllTransactionRequestPaginated(ReadAllTransactionRequestPaginatedObject request, ServerCallContext context)
    {
        return base.ReadAllTransactionRequestPaginated(request, context);
    }

    public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        var command = new CreateCommand {
            AccountId = request.AccountId.Value,
            IncreasedAmount = request.IncreasedAmount.Value,
            DecreasedAmount = request.DecreasedAmount.Value,
            TransactionType = (TransactionType)request.TransactionType.Value
        };

        var result = await mediator.DispatchAsync(command, context.CancellationToken);

        return new() {
            Code = configuration.GetSuccessCreateStatusCode(),
            Message = configuration.GetSuccessCreateMessage(),
            Body = new CreateResponseBody { BankGatewayUrl = result }
        };
    }

    public override async Task<PaymentVerificationResponse> PaymentVerification(PaymentVerificationRequest request,
        ServerCallContext context
    )
    {
        var command = new PaymentVerificationCommand {
            Amount = request.Amount.Value,
            BankGatewaySecretKey = request.BankGatewaySecretKey.Value
        };

        var result = await mediator.DispatchAsync(command, context.CancellationToken);

        return new() {
            Code = configuration.GetSuccessStatusCode(),
            Message = configuration.GetSuccessUpdateMessage(),
            Body = new PaymentVerificationResponseBody {
                Status = result.Status , TransactionNumber = new String { Value = result.TransactionNumber }
            }
        };
    }

    public override async Task<CreateTransactionRequestResponse> CreateTransactionRequest(
        CreateTransactionRequestObject request, ServerCallContext context
    )
    {
        var command = new CreateTransactionRequestCommand {
            AccountId = request.AccountId.Value,
            Amount = request.Amount.Value
        };

        var result = await mediator.DispatchAsync(command, context.CancellationToken);

        return new() {
            Code = configuration.GetSuccessCreateStatusCode(),
            Message = configuration.GetSuccessCreateMessage(),
            Body = new CreateTransactionRequestResponseBody { Result = result }
        };
    }

    public override async Task<ChangeStatusTransactionRequestResponse> ChangeStatusTransactionRequest(
        ChangeStatusTransactionRequestObject request, ServerCallContext context
    )
    {
        var command = new ChangeStatusTransactionRequestCommand {
            Id = request.Id.Value,
            Status = (TransactionStatus)request.Status.Value,
            RejectReason = request.RejectReason.Value,
            BankTransferReceiptImage = request.BankTransferReceiptImage.Value
        };
        
        var result = await mediator.DispatchAsync(command, context.CancellationToken);
        
        return new() {
            Code = configuration.GetSuccessStatusCode(),
            Message = configuration.GetSuccessUpdateMessage(),
            Body = new ChangeStatusTransactionRequestResponseBody { Result = result }
        };
    }

    public override async Task<ChangeAmountTransactionRequestResponse> ChangeAmountTransactionRequest(
        ChangeAmountTransactionRequestObject request, ServerCallContext context
    )
    {
        var command = new ChangeAmountTransactionRequestCommand {
            Id = request.Id.Value,
            Amount = request.Amount.Value
        };
        
        var result = await mediator.DispatchAsync(command, context.CancellationToken);
        
        return new() {
            Code = configuration.GetSuccessStatusCode(),
            Message = configuration.GetSuccessUpdateMessage(),
            Body = new ChangeAmountTransactionRequestResponseBody { Result = result }
        };
    }

    public override async Task<DecreaseBalanceOfWalletResponse> DecreaseBalanceOfWallet(
        DecreaseBalanceOfWalletRequest request, ServerCallContext context
    )
    {
        var command = new DecreaseBalanceCommand {
            Value = request.Value.Value,
            AccountId = request.AccountId.Value
        };

        var result = await mediator.DispatchAsync(command, context.CancellationToken);
        
        return new() {
            Code = configuration.GetSuccessStatusCode(),
            Message = configuration.GetSuccessUpdateMessage(),
            Body = new DecreaseBalanceOfWalletResponseBody { Result = result }
        };
    }
}