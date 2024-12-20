using Microsoft.AspNetCore.Http;
using SpendLess.Persistence;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using TestProjectAzeriMed.Application.Constants;
using TestProjectAzeriMed.Application.Contracts.Persistence;

namespace TestProjectAzeriMed.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly TestProjectDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UnitOfWork(TestProjectDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save() 
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;

            await _context.SaveChangesAsync(username);
        }
    }
}
