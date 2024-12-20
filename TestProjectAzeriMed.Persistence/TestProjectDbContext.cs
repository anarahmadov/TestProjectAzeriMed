using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectAzeriMed.Persistence
{
    public class TestProjectDbContext : DbContext
    {
        public TestProjectDbContext(DbContextOptions<TestProjectDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestProjectDbContext).Assembly);
        }

    }
}
