using Domic.Core.Common.ClassHelpers;
using Domic.Core.UseCase.Contracts.Abstracts;
using Domic.Core.UseCase.Contracts.Interfaces;
using Domic.Domain.Commons.Enumerations;
using Domic.UseCase.TransactionUseCase.DTOs;

namespace Domic.UseCase.TransactionUseCase.Queries.ReadAllTransactionPaginated;

public class ReadAllTransactionPaginatedQuery : PaginatedQuery, IQuery<PaginatedCollection<TransactionDto>>
{
    public string UserId { get; set; }
    public string SearchText { get; set; }
    public Sort Sort { get; set; }
}