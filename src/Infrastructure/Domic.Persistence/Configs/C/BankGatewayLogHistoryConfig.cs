using Domic.Core.Persistence.Configs;
using Domic.Domain.Transaction.Entities;
using Domic.Domain.Transaction.Enumerations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domic.Persistence.Configs.C;

public class BankGatewayLogHistoryConfig : BaseEntityConfig<BankGatewayLogHistory, string>
{
    public override void Configure(EntityTypeBuilder<BankGatewayLogHistory> builder)
    {
        base.Configure(builder);
        
        /*-----------------------------------------------------------*/
        
        //Configs
        
        builder.Property(entity => entity.Status)
               .HasConversion(new EnumToNumberConverter<BankGatewayStatus, byte>())
               .IsRequired();
        
        builder.Property(entity => entity.Type)
               .HasConversion(new EnumToNumberConverter<BankGatewayType, byte>())
               .IsRequired();
        
        /*-----------------------------------------------------------*/

        //Relations
        
        builder.HasOne(lh => lh.Transaction)
               .WithMany(transaction => transaction.LogHistories)
               .HasForeignKey(lh => lh.TransactionId);
    }
}