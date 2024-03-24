using Domic.Core.Persistence.Configs;
using Domic.Domain.GiftTransaction.Entities;
using Domic.Domain.GiftTransaction.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domic.Persistence.Configs.C;

public class GiftTransactionConfig : BaseEntityConfig<GiftTransaction, string>
{
    public override void Configure(EntityTypeBuilder<GiftTransaction> builder)
    {
        base.Configure(builder);
        
        /*-----------------------------------------------------------*/
        
        //Configs

        builder.ToTable("GiftTransactions");

        builder.Property(GiftTransaction => GiftTransaction.AccountId).IsRequired();
        
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

        builder.HasOne(GiftTransaction => GiftTransaction.Account)
               .WithMany(account => account.GiftTransactions)
               .HasForeignKey(GiftTransaction => GiftTransaction.AccountId);
        
        builder.HasOne(GiftTransaction => GiftTransaction.Parent)
               .WithMany(GiftTransaction => GiftTransaction.Childs)
               .HasForeignKey(GiftTransaction => GiftTransaction.TransactionId);
    }
}