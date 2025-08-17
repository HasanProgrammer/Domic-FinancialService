using System.Linq.Expressions;
using Domic.Core.Common.ClassExtensions;
using Domic.Core.Common.ClassHelpers;
using Domic.Core.Domain.Enumerations;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Commons.Enumerations;
using Domic.Domain.Transaction.Contracts.Interfaces;
using Domic.Domain.Transaction.Entities;
using Domic.Domain.Transaction.Enumerations;
using Domic.UseCase.TransactionUseCase.DTOs;

namespace Domic.UseCase.TransactionUseCase.Queries.ReadAllTransactionPaginated;

public class ReadAllTransactionPaginatedQueryHandler(ITransactionCommandRepository transactionCommandRepository) 
    : IQueryHandler<ReadAllTransactionPaginatedQuery, PaginatedCollection<TransactionDto>>
{
    public async Task<PaginatedCollection<TransactionDto>> HandleAsync(ReadAllTransactionPaginatedQuery query, 
        CancellationToken cancellationToken
    )
    {
        Expression<Func<Transaction, bool>> predictCondition =
            transaction => 
                ( string.IsNullOrEmpty(query.UserId)     || transaction.Account.UserId == query.UserId ) &&
                ( string.IsNullOrEmpty(query.SearchText) || transaction.Account.Owner.Contains(query.SearchText) );

        var countOfData = await transactionCommandRepository.CountRowsConditionallyAsync(predictCondition, cancellationToken);

        var collection = await transactionCommandRepository.FindAllWithPaginateAndOrderingByProjectionConditionallyAsync(
            query.PageNumber.Value,
            query.CountPerPage.Value,
            Order.Date,
            query.Sort == Sort.Oldest,
            cancellationToken,
            transaction => new TransactionDto {
                Owner = transaction.Account.Owner,
                Type = transaction.TransactionType == TransactionType.IncreasedAmount ? "شارژ" : "برداشت",
                Amount = transaction.TransactionType == TransactionType.DecreasedAmount 
                    ? transaction.DecreasedAmount.Value.Value
                    : transaction.IncreasedAmount.Value.Value,
                EnCreatedAt = transaction.CreatedAt.EnglishDate.Value,
                FrCreatedAt = transaction.CreatedAt.PersianDate
            },
            predictCondition
        );

        return collection.ToPaginatedCollection(countOfData, query.PageNumber.Value, query.CountPerPage.Value, false);
    }
}