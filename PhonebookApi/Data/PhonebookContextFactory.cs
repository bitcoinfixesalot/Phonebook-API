using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookApi.Data
{
    public class PhonebookContextFactory : IDesignTimeDbContextFactory<PhonebookContext>
    {
        public PhonebookContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();
            var builder = new DbContextOptionsBuilder<PhonebookContext>();
            var connectionString = configuration.GetConnectionString("PhonebookConnectionString");
            builder.UseSqlServer(connectionString);
            return new PhonebookContext(builder.Options);
        }
    }
}
