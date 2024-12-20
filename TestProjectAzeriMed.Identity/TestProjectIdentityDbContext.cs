using Microsoft.EntityFrameworkCore;
using TestProjectAzeriMed.Identity.Models;

namespace TestProjectAzeriMed.Identity
{
    public class TestProjectIdentityDbContext : DbContext
    {
        public TestProjectIdentityDbContext(DbContextOptions<TestProjectIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestProjectIdentityDbContext).Assembly);
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        
    }
}
