using Microsoft.AspNetCore.Mvc;
using TestProjectAzeriMed.Application.Contracts.Identity;
using TestProjectAzeriMed.Application.Models.Identity;

namespace TestProjectAzeriMed.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public async Task<IActionResult> Login(AuthRequest authRequest, bool isTokenExpired = false)
        {
            var response = await _authService.Login(authRequest, isTokenExpired);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest registrationRequest)
        {
            var response = await _authService.Register(registrationRequest);

            return Ok(response);
        }
    }
}
