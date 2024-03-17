using System.Data;
using Domic.Domain.Commons.Contracts.Interfaces;
using Domic.Persistence.Contexts.C;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domic.Infrastructure.Implementations.Domain.Repositories.C;

public class CommandUnitOfWork(SQLContext context) : ICommandUnitOfWork
{
    private IDbContextTransaction _transaction;

    public void Transaction(IsolationLevel isolationLevel) 
        => _transaction = context.Database.BeginTransaction(isolationLevel); //Resource

    public void Commit()
    {
        context.SaveChanges();
        _transaction.Commit();
    }

    public void Rollback() => _transaction?.Rollback();

    public void Dispose() => _transaction?.Dispose();
}