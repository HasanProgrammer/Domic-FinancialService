using Domic.Core.Persistence.Configs;
using Domic.Domain.Account.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domic.Persistence.Configs.C;

public class AccountConfig : BaseEntityConfig<Account, string>
{
    public override void Configure(EntityTypeBuilder<Account> builder)
    {
        base.Configure(builder);
        
        /*-----------------------------------------------------------*/
        
        //Configs

        builder.ToTable("Accounts");

        builder.Property(account => account.UserId).IsRequired();
        
        builder.OwnsOne(account => account.Balance)
               .Property(account => account.Value)
               .IsRequired()
               .HasColumnName("Balance");
        
        /*-----------------------------------------------------------*/
        
        //Relations
        
        builder.HasMany(account => account.Transactions)
               .WithOne(GiftTransaction => GiftTransaction.Account)
               .HasForeignKey(GiftTransaction => GiftTransaction.AccountId);
    }
}