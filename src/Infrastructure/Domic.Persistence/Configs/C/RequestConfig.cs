using Domic.Core.Persistence.Configs;
using Domic.Domain.Transaction.Entities;
using Domic.Domain.Transaction.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domic.Persistence.Configs.C;

public class RequestConfig : BaseEntityConfig<Request, string>
{
    public override void Configure(EntityTypeBuilder<Request> builder)
    {
        base.Configure(builder);
        
        /*-----------------------------------------------------------*/
        
        //Configs

        builder.ToTable("Requests");

        builder.Property(entity => entity.RejectReason).IsRequired(false);
        builder.Property(entity => entity.BankTransferReceiptImage).IsRequired(false);
        
        builder.Property(entity => entity.Status)
               .HasConversion(new EnumToNumberConverter<TransactionStatus, byte>())
               .IsRequired();
        
        builder.OwnsOne(account => account.Amount)
               .Property(account => account.Value)
               .HasColumnName("Amount");
        
        /*-----------------------------------------------------------*/
        
        //Relations

        builder.HasOne(request => request.Account)
               .WithMany(account => account.Requests)
               .HasForeignKey(request => request.AccountId);
    }
}