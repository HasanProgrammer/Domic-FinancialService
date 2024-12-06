using Microsoft.AspNetCore.Mvc;

namespace Domic.WebAPI.EntryPoints.HTTPs;

[Route("[controller]/[action]")]
public class FinancialController : ControllerBase
{
    [HttpGet]
    public IActionResult BankGatewayResponse() 
        => Ok(new {
            Amount = HttpContext.Request.Query["Amount"],
            GatewaySecretCode = HttpContext.Request.Query["Authority"],
        });
}