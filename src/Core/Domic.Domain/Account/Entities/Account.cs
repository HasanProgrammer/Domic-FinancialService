#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;
using Domic.Core.Domain.ValueObjects;
using Domic.Domain.Account.Events;
using Domic.Domain.Commons.ValueObjects;
using Domic.Domain.Transaction.Entities;

namespace Domic.Domain.Account.Entities;

public class Account : Entity<string>
{
    public string UserId { get; private set; }
    
    //Value Objects
    
    public Amount Balance { get; private set; }

    /*---------------------------------------------------------------*/
    
    //Relations
    
    public ICollection<Request> Requests { get; set; }
    
    public ICollection<Transaction.Entities.Transaction> Transactions { get; set; }

    /*---------------------------------------------------------------*/

    //EF Core
    private Account() {}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="globalUniqueIdGenerator"></param>
    /// <param name="dateTime"></param>
    /// <param name="userId"></param>
    /// <param name="createdBy"></param>
    /// <param name="createdRole"></param>
    /// <param name="balance"></param>
    public Account(IGlobalUniqueIdGenerator globalUniqueIdGenerator, IDateTime dateTime, string userId,
        string createdBy, string createdRole, long balance
    )
    {
        var uniqueId = globalUniqueIdGenerator.GetRandom(6);
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id = uniqueId;
        UserId = userId;
        Balance = new Amount(balance);
        
        //audit
        CreatedRole = createdRole;
        CreatedBy = createdBy;
        CreatedAt = new CreatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new AccountCreated {
                Id = uniqueId,
                UserId = userId,
                CreatedBy = createdBy,
                CreatedRole = createdRole,
                Balance = balance,
                CreatedAt_EnglishDate = nowDateTime,
                CreatedAt_PersianDate = nowPersianDateTime
            }
        );
    }

    /*---------------------------------------------------------------*/
    
    //Behaviors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="updatedBy"></param>
    /// <param name="updatedRole"></param>
    public void Active(IDateTime dateTime, string updatedBy, string updatedRole)
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);
        
        IsActive = IsActive.Active;
        UpdatedBy = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new AccountActived {
                Id = Id,
                UpdatedBy = updatedBy,
                UpdatedRole = updatedRole,
                UpdatedAt_EnglishDate = nowDateTime,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="updatedBy"></param>
    /// <param name="updatedRole"></param>
    public void InActive(IDateTime dateTime, string updatedBy, string updatedRole)
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);
        
        IsActive = IsActive.InActive;
        
        //audit
        UpdatedBy = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new AccountInActived {
                Id = Id,
                UpdatedBy = updatedBy,
                UpdatedRole = updatedRole,
                UpdatedAt_EnglishDate = nowDateTime,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
    
    public void IncreaseBalance(IDateTime dateTime, IIdentityUser identityUser, ISerializer serializer, long value)
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);
        
        Balance = new Amount(Balance.Value + value);
        
        //audit
        UpdatedBy = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new AccountCharged {
                Id = Id,
                Value = value,
                UpdatedBy = UpdatedBy,
                UpdatedRole = UpdatedRole,
                UpdatedAt_EnglishDate = nowDateTime,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
    
    public void DecreaseBalance(IDateTime dateTime, IIdentityUser identityUser, ISerializer serializer, long value)
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);
        
        Balance = new Amount(Balance.Value - value);
        
        //audit
        UpdatedBy = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new AccountDeCharged {
                Id = Id,
                Value = value,
                UpdatedBy = UpdatedBy,
                UpdatedRole = UpdatedRole,
                UpdatedAt_EnglishDate = nowDateTime,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
}