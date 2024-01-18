using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Karami.Persistence.Contexts.C;

public class SQLContextFactory : IDesignTimeDbContextFactory<SQLContext>
{
    public SQLContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<SQLContext>();
        
        builder.UseSqlServer("Somethings!");

        return new SQLContext(builder.Options);
    }
}