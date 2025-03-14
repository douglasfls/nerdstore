using Microsoft.AspNetCore.Mvc;

namespace Nerdstore.Customers.WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class CustomersController: ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("teste");
    }
}