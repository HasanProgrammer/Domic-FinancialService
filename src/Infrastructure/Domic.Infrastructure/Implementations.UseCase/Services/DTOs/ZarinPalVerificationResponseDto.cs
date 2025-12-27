namespace Domic.Infrastructure.Implementations.UseCase.Services.DTOs;

public class ZarinPalVerificationResponseDto
{
    public VerifyData data { get; set; }
}

public class VerifyData
{
    public int code { get; set; }
    public string message { get; set; }
    public string card_hash { get; set; }
    public string card_pan { get; set; }
    public long ref_id { get; set; }
    public string fee_type { get; set; }
    public long fee { get; set; }
}