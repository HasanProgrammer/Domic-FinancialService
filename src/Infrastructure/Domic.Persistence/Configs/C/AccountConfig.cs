using Domic.Domain.Service.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domic.Persistence.Configs.C;

public class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        //PrimaryKey
        
        builder.HasKey(account => account.Id);

        builder.ToTable("Accounts");
        
        /*-----------------------------------------------------------*/

        //Property

        /*-----------------------------------------------------------*/
        
        //Relations
    }
}