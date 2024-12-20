using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestProjectAzeriMed.Application.Contracts.Identity;
using TestProjectAzeriMed.Application.Models.Identity;
using TestProjectAzeriMed.Identity.Helpers;
using TestProjectAzeriMed.Identity.Models;

namespace TestProjectAzeriMed.Identity.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly TestProjectIdentityDbContext _dbContext;
        private readonly DbSet<ApplicationUser> _dbSet;
        private readonly IPasswordHelper _passwordHelper;
        private readonly ITokenService _tokenService;

        public AuthService(TestProjectIdentityDbContext dbContext,
                            IOptions<JwtSettings> jwtSettings,
                            IPasswordHelper passwordHelper,
                            ITokenService tokenService)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<ApplicationUser>();
            _jwtSettings = jwtSettings.Value;
            _passwordHelper = passwordHelper;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> Login(AuthRequest request)
        {
            var user = await _dbSet.FirstOrDefaultAsync(x => x.Name == request.Name);

            if (user == null)
            {
                throw new Exception($"User {request.Name} not found.");
            }

            var result = _passwordHelper.VerifyPassword(new ApplicationUser(), user.PasswordHash, request.Password);

            if (!result)
            {
                throw new Exception($"Credentials for '{request.Name} aren't valid'.");
            }

            AuthResponse response = new AuthResponse
            {
                Token = user.Token
            };

            return response;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest request)
        {
            var existingUser = await _dbSet.FirstOrDefaultAsync(x => x.Name == request.Name);

            if (existingUser != null)
            {
                throw new Exception($"Username '{request.Name}' already exists.");
            }

            string password_hash = _passwordHelper.HashPassword(new ApplicationUser(), request.Password);

            var user = new ApplicationUser
            {
                Name = request.Name,
                DateOfBirth = request.DateOfBirth,
                PasswordHash = password_hash
            };

            var tokenString = _tokenService.GenerateToken(user.ID, user.Name, expiryMinutes: 60);

            try
            {
                await _dbSet.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                var userEntity = await _dbSet.FirstOrDefaultAsync( x => x.Name == request.Name);
                userEntity.Token = tokenString;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return new RegistrationResponse() {Token = tokenString };
        }

        //private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        //{
        //    var userClaims = await _userManager.GetClaimsAsync(user);
        //    var roles = await _userManager.GetRolesAsync(user);

        //    var roleClaims = new List<Claim>();

        //    for (int i = 0; i < roles.Count; i++)
        //    {
        //        roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
        //    }

        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        //        new Claim(CustomClaimTypes.Uid, user.Id)
        //    }
        //    .Union(userClaims)
        //    .Union(roleClaims);

        //    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        //    var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        //    var jwtSecurityToken = new JwtSecurityToken(
        //        issuer: _jwtSettings.Issuer,
        //        audience: _jwtSettings.Audience,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
        //        signingCredentials: signingCredentials);
        //    return jwtSecurityToken;
        //}
    }
}
