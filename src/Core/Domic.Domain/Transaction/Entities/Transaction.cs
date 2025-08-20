#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;
using Domic.Core.Domain.ValueObjects;
using Domic.Domain.Commons.ValueObjects;
using Domic.Domain.Transaction.Enumerations;
using Domic.Domain.Transaction.Events;

namespace Domic.Domain.Transaction.Entities;

public class Transaction : Entity<string>
{
    public string AccountId { get; private set; }
    public TransactionType TransactionType { get; private set; }
    
    /*---------------------------------------------------------------*/
    
    //Value Objects
    
    public Amount IncreasedAmount { get; private set; }
    public Amount DecreasedAmount { get; private set; }

    /*---------------------------------------------------------------*/
    
    //Relations
    
    public Account.Entities.Account Account { get; set; }
    
    public ICollection<BankGatewayLogHistory> LogHistories { get; set; }

    /*---------------------------------------------------------------*/

    //EF Core
    private Transaction() {}
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    /// <param name="globalUniqueIdGenerator"></param>
    /// <param name="dateTime"></param>
    /// <param name="accountId"></param>
    /// <param name="increasedAmount"></param>
    /// <param name="decreasedAmount"></param>
    /// <param name="transactionType"></param>
    public Transaction(IIdentityUser identityUser, ISerializer serializer, 
        IGlobalUniqueIdGenerator globalUniqueIdGenerator, IDateTime dateTime, string accountId, long? increasedAmount,
        long? decreasedAmount, TransactionType transactionType
    )
    {
        var uniqueId = globalUniqueIdGenerator.GetRandom(6);
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id = uniqueId;
        AccountId = accountId;
        TransactionType = transactionType;
        IsActive = IsActive.Active;
        
        if(increasedAmount is not null)
            IncreasedAmount = new Amount(increasedAmount);
        
        if(decreasedAmount is not null)
            DecreasedAmount = new Amount(decreasedAmount);
        
        //audit
        CreatedBy = identityUser.GetIdentity();
        CreatedRole = serializer.Serialize(identityUser.GetRoles());
        CreatedAt = new CreatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TransactionCreated {
                Id = uniqueId,
                AccountId = accountId,
                TransactionType = transactionType,
                IncreasedAmount = increasedAmount,
                DecreasedAmount = decreasedAmount,
                CreatedBy = CreatedBy,
                CreatedRole = CreatedRole,
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
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    /// <param name="withRaisingEvent"></param>
    public void Active(IDateTime dateTime, IIdentityUser identityUser, ISerializer serializer, bool withRaisingEvent = false)
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsActive = IsActive.Active;
        
        //audit
        UpdatedBy = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);

        if (withRaisingEvent)
        {
            //Throw event
        }
    }
    
    /// <summary>
    /// call from consumer handler
    /// </summary>
    /// <param name="en_updatedAt"></param>
    /// <param name="pr_updatedAt"></param>
    /// <param name="updatedBy"></param>
    /// <param name="updateRole"></param>
    public void Active(DateTime en_updatedAt, string pr_updatedAt, string updatedBy, string updatedRole)
    {
        IsActive = IsActive.Active;
        
        //audit
        UpdatedBy = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt = new UpdatedAt(en_updatedAt, pr_updatedAt);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="identityUser"></param>
    /// <param name="serializer"></param>
    /// <param name="withRaisingEvent"></param>
    public void InActive(IDateTime dateTime, IIdentityUser identityUser, ISerializer serializer, bool withRaisingEvent = false)
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsActive = IsActive.InActive;
        
        //audit
        UpdatedBy = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if (withRaisingEvent)
        {
            //Throw event
        }
    }
    
    /// <summary>
    /// call from consumer handler
    /// </summary>
    /// <param name="en_updatedAt"></param>
    /// <param name="pr_updatedAt"></param>
    /// <param name="updatedBy"></param>
    /// <param name="updateRolee"></param>
    public void InActive(DateTime en_updatedAt, string pr_updatedAt, string updatedBy, string updatedRole)
    {
        IsActive = IsActive.InActive;
        
        //audit
        UpdatedBy = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt = new UpdatedAt(en_updatedAt, pr_updatedAt);
    }
}