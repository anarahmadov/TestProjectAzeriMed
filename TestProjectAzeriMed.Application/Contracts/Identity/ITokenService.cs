using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectAzeriMed.Application.Models.Identity;

namespace TestProjectAzeriMed.Application.Contracts.Identity
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string name, int expiryMinutes = 60);
        CustomToken? ValidateToken(string token);
    }
}
