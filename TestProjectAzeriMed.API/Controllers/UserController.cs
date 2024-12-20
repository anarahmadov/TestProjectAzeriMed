using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestProjectAzeriMed.API.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomScheme")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public IActionResult GetUser(int userId)
        {


            return View();
        }
    }
}
