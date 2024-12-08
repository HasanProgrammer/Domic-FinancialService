namespace Domic.Domain.Transaction.Enumerations;

public enum BankGatewayStatus : byte
{
    SendRequest, AcceptRequest, ErrorVerificationPurchase, SuccessVerificationPurchase
}