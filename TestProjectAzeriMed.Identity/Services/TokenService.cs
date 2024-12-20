using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TestProjectAzeriMed.Application.Contracts.Identity;
using TestProjectAzeriMed.Application.Models.Identity;

namespace TestProjectAzeriMed.Identity.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(int userId, string name, int expiryMinutes = 60)
        {
            var tokenData = new CustomToken
            {
                UserId = userId,
                Name = name,
                ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes)
            };

            var payload = JsonSerializer.Serialize(tokenData);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(payload));
        }

        public CustomToken? ValidateToken(string token)
        {
            try
            {
                var payload = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                var tokenData = JsonSerializer.Deserialize<CustomToken>(payload);

                // Check expiration
                if (tokenData == null || tokenData.ExpiresAt < DateTime.UtcNow)
                    return null;

                return tokenData;
            }
            catch
            {
                return null;
            }
        }
    }
}
