using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Contracts.Interfaces;
using Domic.Core.Domain.ValueObjects;
using Domic.Domain.Transaction.Enumerations;
using Domic.Domain.Transaction.Events;

namespace Domic.Domain.Transaction.Entities;

public class BankGatewayLogHistory : Entity<string>
{
    public string TransactionId { get; private set; }
    public BankGatewayType Type { get; private set; }
    public BankGatewayStatus Status { get; private set; }
    public string SecretConnectionKey { get; private set; }
    public string TransactionNumber { get; private set; }
        
    /*---------------------------------------------------------------*/
    
    //Relations
    
    public Transaction Transaction { get; set; }
    
    /*---------------------------------------------------------------*/

    //EF Core
    public BankGatewayLogHistory(){}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identityUser"></param>
    /// <param name="dateTime"></param>
    /// <param name="globalUniqueIdGenerator"></param>
    /// <param name="serializer"></param>
    /// <param name="transactionId"></param>
    /// <param name="type"></param>
    /// <param name="status"></param>
    /// <param name="secretConnectionKey"></param>
    public BankGatewayLogHistory(IIdentityUser identityUser, IDateTime dateTime,
        IGlobalUniqueIdGenerator globalUniqueIdGenerator, ISerializer serializer, string transactionId, 
        BankGatewayType type, BankGatewayStatus status, string secretConnectionKey, string transactionNumber
    )
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Id = globalUniqueIdGenerator.GetRandom(6);
        TransactionId = transactionId;
        Type = type;
        Status = status;
        SecretConnectionKey = secretConnectionKey;
        TransactionNumber = transactionNumber;
        
        //audit
        CreatedBy = identityUser.GetIdentity();
        CreatedRole = serializer.Serialize(identityUser.GetRoles());
        CreatedAt = new CreatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new BankGatewayLogHistoryCreated {
                Id = Id,
                TransactionId = transactionId,
                Status = (int)status,
                Type = (int)type,
                SecretConnectionKey = secretConnectionKey,
                TransactionNumber = transactionNumber,
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
    /// <param name="serializer"></param>
    /// <param name="dateTime"></param>
    /// <param name="transactionId"></param>
    /// <param name="status"></param>
    public void ChangeStatus(IIdentityUser identityUser, ISerializer serializer, IDateTime dateTime,
        string transactionId, BankGatewayStatus status
    )
    {
        var nowDateTime = DateTime.Now;
        var nowPersianDateTime = dateTime.ToPersianShortDate(nowDateTime);

        Status = status;
        
        //audit
        UpdatedBy = identityUser.GetIdentity();
        UpdatedRole = serializer.Serialize(identityUser.GetRoles());
        UpdatedAt = new UpdatedAt(nowDateTime, nowPersianDateTime);
        
        AddEvent(
            new BankGatewayLogHistoryStatusChanged {
                Id = Id,
                TransactionId = transactionId,
                Status = (int)status,
                UpdatedBy = CreatedBy,
                UpdatedRole = CreatedRole,
                UpdatedAt_EnglishDate = nowDateTime,
                UpdatedAt_PersianDate = nowPersianDateTime
            }
        );
    }
}