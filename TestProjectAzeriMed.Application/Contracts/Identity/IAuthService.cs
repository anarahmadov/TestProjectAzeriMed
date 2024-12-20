using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProjectAzeriMed.Application.Models.Identity;

namespace TestProjectAzeriMed.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request, bool isTokenExpired = false);
        Task<RegistrationResponse> Register(RegistrationRequest request);

    }
}
