#pragma warning disable CS0649

using Domic.Domain.Service.Events;
using Domic.Domain.Service.ValueObjects;
using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;
using Domic.Core.Domain.ValueObjects;

namespace Domic.Domain.Service.Entities;

public class Account : Entity<string>
{
    //Value Objects
    
    public Balance Balance { get; private set; }

    /*---------------------------------------------------------------*/
    
    //Relations

    /*---------------------------------------------------------------*/

    //EF Core
    private Account() {}

    public Account(IGlobalUniqueIdGenerator globalUniqueIdGenerator, IDateTime dateTime, long balance)
    {
        var uniqueId = globalUniqueIdGenerator.GetRandom();
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id = uniqueId;
        CreatedBy = uniqueId;
        Balance = new Balance(balance);
        CreatedAt = new CreatedAt(nowDateTime, nowPersianDateTime);
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new AccountCreated {
                Id = uniqueId,
                CreatedBy = uniqueId,
                CreatedRole = "",
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
    public void Active(IDateTime dateTime, string updatedBy)
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);
        
        IsActive = IsActive.Active;
        UpdatedBy = updatedBy;
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new AccountActived {
                Id = Id,
                UpdatedBy = updatedBy,
                UpdatedRole = "",
                IsActive = IsActive == IsActive.Active,
                UpdatedAt_EnglishDate = nowDateTime,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
    
    /// <summary>
    /// 
    /// </summary>
    public void InActive(IDateTime dateTime, string updatedBy)
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);
        
        IsActive = IsActive.InActive;
        UpdatedBy = updatedBy;
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new AccountInActived {
                Id = Id,
                UpdatedBy = updatedBy,
                UpdatedRole = "",
                IsActive = IsActive == IsActive.Active,
                UpdatedAt_EnglishDate = nowDateTime,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
}