using Microsoft.AspNetCore.Mvc;

namespace EmployeesCatalog.Web.controllers
{
    [Route("/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }
}