#pragma warning disable CS0649

using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.Enumerations;
using Domic.Core.Domain.ValueObjects;
using Domic.Domain.Commons.ValueObjects;
using Domic.Domain.GiftTransaction.Enumerations;
using Domic.Domain.GiftTransaction.Events;

namespace Domic.Domain.GiftTransaction.Entities;

public class GiftTransaction : Entity<string>
{
    public string AccountId { get; private set; }
    public string TransactionId { get; private set; }
    public TransactionType TransactionType { get; private set; }
    
    /*---------------------------------------------------------------*/
    
    //Value Objects
    
    public Amount IncreasedAmount { get; private set; }
    public Amount DecreasedAmount { get; private set; }

    /*---------------------------------------------------------------*/
    
    //Relations
    
    public Account.Entities.Account Account { get; set; }
    
    public GiftTransaction Parent { get; set; }
    
    public ICollection<GiftTransaction> Childs { get; set; }

    /*---------------------------------------------------------------*/

    //EF Core
    private GiftTransaction() {}
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="globalUniqueIdGenerator"></param>
    /// <param name="dateTime"></param>
    /// <param name="accountId"></param>
    /// <param name="transactionId"></param>
    /// <param name="increasedAmount"></param>
    /// <param name="decreasedAmount"></param>
    /// <param name="transactionType"></param>
    /// <param name="createdBy"></param>
    /// <param name="createdRole"></param>
    public GiftTransaction(IGlobalUniqueIdGenerator globalUniqueIdGenerator, IDateTime dateTime, string accountId,
        string transactionId, long? increasedAmount, long? decreasedAmount, TransactionType transactionType, 
        string createdBy, string createdRole
    )
    {
        var uniqueId = globalUniqueIdGenerator.GetRandom(6);
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id = uniqueId;
        AccountId = accountId;
        TransactionId = transactionId;
        CreatedBy = createdBy;
        CreatedRole = createdRole;
        
        if(increasedAmount is not null)
            IncreasedAmount = new Amount(increasedAmount);
        
        if(decreasedAmount is not null)
            DecreasedAmount = new Amount(decreasedAmount);
        
        CreatedAt = new CreatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new GiftTransactionCreated {
                Id = uniqueId,
                AccountId = accountId,
                GiftTransactionId = transactionId,
                TransactionType = transactionType,
                IncreasedAmount = increasedAmount,
                DecreasedAmount = decreasedAmount,
                CreatedBy = createdBy,
                CreatedRole = createdRole,
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
    /// <param name="withRaisingEvent"></param>
    public void Active(IDateTime dateTime, string updatedBy, string updatedRole, bool withRaisingEvent = false)
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsActive = IsActive.Active;
        UpdatedBy = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);

        if (withRaisingEvent)
        {
            //Throw event
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="updatedBy"></param>
    /// <param name="updatedRole"></param>
    /// <param name="withRaisingEvent"></param>
    public void InActive(IDateTime dateTime, string updatedBy, string updatedRole, bool withRaisingEvent = false)
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        IsActive = IsActive.InActive;
        UpdatedBy = updatedBy;
        UpdatedRole = updatedRole;
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        if (withRaisingEvent)
        {
            //Throw event
        }
    }
}