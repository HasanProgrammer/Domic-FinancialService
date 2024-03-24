using Domic.Core.Persistence.Configs;
using Domic.Domain.Transaction.Entities;
using Domic.Domain.Transaction.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domic.Persistence.Configs.C;

public class TransactionConfig : BaseEntityConfig<Transaction, string>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);
        
        /*-----------------------------------------------------------*/
        
        //Configs

        builder.ToTable("Transactions");

        builder.Property(transaction => transaction.AccountId).IsRequired();
        
        builder.Property(entity => entity.TransactionType)
               .HasConversion(new EnumToNumberConverter<TransactionType, byte>())
               .IsRequired();
        
        builder.OwnsOne(account => account.IncreasedAmount)
               .Property(account => account.Value)
               .HasColumnName("IncreasedAmount");
        
        builder.OwnsOne(account => account.DecreasedAmount)
               .Property(account => account.Value)
               .HasColumnName("DecreasedAmount");
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasOne(transaction => transaction.Account)
               .WithMany(account => account.Transactions)
               .HasForeignKey(transaction => transaction.AccountId);
        
        builder.HasOne(transaction => transaction.Parent)
               .WithMany(transaction => transaction.Childs)
               .HasForeignKey(transaction => transaction.TransactionId);
    }
}