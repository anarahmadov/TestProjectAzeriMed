using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using TestProjectAzeriMed.Application.Contracts.Identity;

namespace TestProjectAzeriMed.API.Handlers
{
    public class CustomAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ITokenService _tokenService;

        public CustomAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ITokenService tokenService)
            : base(options, logger, encoder, clock)
        {
            _tokenService = tokenService;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Token is missing"));
            }

            var validatedToken = _tokenService.ValidateToken(token);

            if (validatedToken == null)
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid or expired token"));
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.PrimarySid, validatedToken.UserId.ToString()),
                new Claim(ClaimTypes.Name, validatedToken.Name)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
