﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SpendLess.Identity.Services;
using TestProjectAzeriMed.Application.Contracts.Identity;
using TestProjectAzeriMed.Identity.Helpers;
using TestProjectAzeriMed.Identity.Models;
using TestProjectAzeriMed.Identity.Services;

namespace TestProjectAzeriMed.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestProjectIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TestProjectIdentityConnectionString"),
                    sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,                           // Max retry attempts
                            maxRetryDelay: TimeSpan.FromSeconds(10),    // Delay between retries
                            errorNumbersToAdd: new List<int> { 10060 }  // Example of adding specific error codes
                        );
                    });
            });
            services.AddTransient<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
            services.AddTransient<IPasswordHelper, PasswordHelper>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();

            services.AddSingleton<ITokenService, TokenService>();

            return services;
        }
    }
}
