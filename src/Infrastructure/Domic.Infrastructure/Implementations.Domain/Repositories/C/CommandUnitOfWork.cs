using System.Data;
using Domic.Domain.Commons.Contracts.Interfaces;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class CommandUnitOfWork(SQLContext context) : ICommandUnitOfWork
{
    private IDbContextTransaction _GiftTransaction;

    public void GiftTransaction(IsolationLevel isolationLevel) 
        => _GiftTransaction = context.Database.BeginTransaction(isolationLevel); //Resource

    public void Commit()
    {
        context.SaveChanges();
        _GiftTransaction.Commit();
    }

    public void Rollback() => _GiftTransaction?.Rollback();

    public void Dispose() => _GiftTransaction?.Dispose();
}