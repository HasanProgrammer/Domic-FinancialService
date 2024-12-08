using Domic.Persistence.Configs.C;
using Domic.Core.Domain.Entities;
using Domic.Core.Persistence.Configs;
using Domic.Domain.Account.Entities;
using Domic.Domain.Transaction.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domic.Persistence.Contexts.C;

/*Setting*/
public partial class SQLContext : DbContext
{
    public SQLContext(DbContextOptions<SQLContext> options) : base(options)
    {
        
    }
}

/*Entity*/
public partial class SQLContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<ConsumerEvent> ConsumerEvents { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<BankGatewayLogHistory> LogHistories { get; set; }
}

/*Config*/
public partial class SQLContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new EventConfig());
        builder.ApplyConfiguration(new ConsumerEventConfig());
        builder.ApplyConfiguration(new AccountConfig());
        builder.ApplyConfiguration(new RequestConfig());
        builder.ApplyConfiguration(new TransactionConfig());
        builder.ApplyConfiguration(new BankGatewayLogHistoryConfig());
    }
}