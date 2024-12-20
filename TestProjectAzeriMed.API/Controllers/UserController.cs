using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestProjectAzeriMed.Application.Contracts.Identity;

namespace TestProjectAzeriMed.API.Controllers
{
    [Authorize(AuthenticationSchemes = "CustomScheme")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery]int userId)
        {
            var user = await _userService.GetUser(userId);

            return Ok(user);
        }
    }
}
