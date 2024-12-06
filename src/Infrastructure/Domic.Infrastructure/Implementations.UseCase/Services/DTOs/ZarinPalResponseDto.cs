namespace Domic.Infrastructure.Implementations.UseCase.Services.DTOs;

public class ZarinPalResponseDto
{
    public RequestData data { get; set; }
    public List<string> errors { get; set; }
}

public class RequestData
{
    public int code { get; set; }
    public string message { get; set; }
    public string authority { get; set; }
}