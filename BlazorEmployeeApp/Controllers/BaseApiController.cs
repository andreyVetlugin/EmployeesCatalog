using Microsoft.AspNetCore.Mvc;

namespace EmployeesCatalog.Web.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }
}