using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.ValueObjects;
using Domic.Domain.Commons.ValueObjects;
using Domic.Domain.Transaction.Events;
using TransactionStatus = Domic.Domain.Transaction.Enumerations.TransactionStatus;

namespace Domic.Domain.Transaction.Entities;

public class Request : Entity<string>
{
    public string AccountId { get; private set; }
    public Amount Amount { get; private set; }
    public TransactionStatus Status { get; private set; } = TransactionStatus.Requested;
    public string RejectReason { get; private set; }
    public string BankTransferReceiptImage { get; private set; }
    
    /*---------------------------------------------------------------*/
    
    //Relations
    
    public Account.Entities.Account Account { get; set; }
    
    /*---------------------------------------------------------------*/

    //EF Core
    public Request(){}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="globalUniqueIdGenerator"></param>
    /// <param name="identityUser"></param>
    /// <param name="dateTime"></param>
    /// <param name="serializer"></param>
    /// <param name="accountId"></param>
    /// <param name="amount"></param>
    /// <param name="status"></param>
    /// <param name="rejectReason"></param>
    /// <param name="bankTransferReceiptImage"></param>
    public Request(IGlobalUniqueIdGenerator globalUniqueIdGenerator, IIdentityUser identityUser, IDateTime dateTime,
        ISerializer serializer, string accountId, long? amount, TransactionStatus status, string rejectReason,
        string bankTransferReceiptImage
    )
    {
        var uniqueId = globalUniqueIdGenerator.GetRandom(6);
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id = uniqueId;
        AccountId = accountId;
        Amount = new Amount(amount);
        Status = status;
        RejectReason = rejectReason;
        
        //audit
        CreatedBy = identityUser.GetIdentity();
        CreatedRole = serializer.Serialize(identityUser.GetRoles());
        CreatedAt = new CreatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TransactionRequestCreated {
                Id = Id,
                AccountId = accountId,
                Amount = amount,
                Status = status,
                RejectReason = rejectReason,
                BankTransferReceiptImage = bankTransferReceiptImage,
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
    /// <param name="identityUser"></param>
    /// <param name="dateTime"></param>
    /// <param name="serializer"></param>
    /// <param name="amount"></param>
    public void ChangeAmount(IIdentityUser identityUser, IDateTime dateTime, ISerializer serializer, long? amount)
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);
        
        Amount = new Amount(amount);
        
        //audit
        UpdatedBy = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TransactionRequestUpdatedAmount{
                Id = Id,
                Amount = amount,
                UpdatedBy = CreatedBy,
                UpdatedRole = CreatedRole,
                UpdatedAt_EnglishDate = nowDateTime,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="identityUser"></param>
    /// <param name="dateTime"></param>
    /// <param name="serializer"></param>
    /// <param name="status"></param>
    /// <param name="rejectReason"></param>
    /// <param name="bankTransferReceiptImage"></param>
    public void ChangeStatus(IIdentityUser identityUser, IDateTime dateTime, ISerializer serializer, 
        TransactionStatus status, string rejectReason, string bankTransferReceiptImage
    )
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Status = status;
        
        if(!string.IsNullOrEmpty(rejectReason)) 
            RejectReason = rejectReason;

        if (!string.IsNullOrEmpty(BankTransferReceiptImage))
            BankTransferReceiptImage = bankTransferReceiptImage;
        
        //audit
        UpdatedBy = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new TransactionRequestUpdatedStatus{
                Id = Id,
                Status = status,
                RejectReason = rejectReason,
                UpdatedBy = CreatedBy,
                UpdatedRole = CreatedRole,
                UpdatedAt_EnglishDate = nowDateTime,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
}