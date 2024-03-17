#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;
using Domic.Core.Domain.ValueObjects;
using Domic.Domain.Account.Events;
using Domic.Domain.Commons.ValueObjects;

namespace Domic.Domain.Account.Entities;

public class Account : Entity<string>
{
    public string UserId { get; private set; }
    
    //Value Objects
    
    public Amount Balance { get; private set; }

    /*---------------------------------------------------------------*/
    
    //Relations
    
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
        CreatedBy = createdBy;
        CreatedRole = createdRole;
        Balance = new Amount(balance);
        CreatedAt = new CreatedAt(nowDateTime, nowPersianDateTime);
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
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
}