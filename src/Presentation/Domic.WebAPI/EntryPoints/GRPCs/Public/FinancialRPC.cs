using Domic.Core.Common.ClassExtensions;
using Domic.Core.Financial.Grpc;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Transaction.Enumerations;
using Domic.UseCase.TransactionUseCase.Commands.Create;
using Domic.UseCase.TransactionUseCase.Commands.PaymentVerification;
using Grpc.Core;
using String = Domic.Core.Financial.Grpc.String;

namespace Domic.WebAPI.EntryPoints.GRPCs.Public;

public class FinancialRPC(IMediator mediator, IConfiguration configuration) : FinancialService.FinancialServiceBase
{
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
            Code = configuration.GetSuccessStatusCode(),
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
            Message = configuration.GetSuccessCreateMessage(),
            Body = new PaymentVerificationResponseBody {
                Status = result.Status , TransactionNumber = new String { Value = result.TransactionNumber }
            }
        };
    }
}