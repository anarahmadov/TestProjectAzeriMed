using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestProjectAzeriMed.Identity
{
    public class TestProjectContextFactory : IDesignTimeDbContextFactory<TestProjectIdentityDbContext>
    {
        public TestProjectIdentityDbContext CreateDbContext(string[] args)
        {
            var path = Directory.GetParent(Directory.GetCurrentDirectory());

            var configuration = new ConfigurationBuilder()
                    .SetBasePath($@"{path}/TestProjectAzeriMed.API")
                    .AddJsonFile("appsettings.json")
                    .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TestProjectIdentityDbContext>();

            var connectionString = configuration
                        .GetConnectionString("TestProjectIdentityConnectionString");

            optionsBuilder.UseSqlServer(connectionString);

            return new TestProjectIdentityDbContext(optionsBuilder.Options);
        }
    }
}
