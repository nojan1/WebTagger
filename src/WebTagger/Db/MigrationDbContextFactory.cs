using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Configuration;

namespace WebTagger.Db
{
    public class MigrationDbContextFactory : IDbContextFactory<ApplicationContext>
    {
        public ApplicationContext Create(DbContextFactoryOptions options)
        {
            string connectionString = "";

            var location = Environment.GetEnvironmentVariable("DBLOCATION");
            if (string.IsNullOrEmpty(location))
            {
                var configurationProvider = new ConfigurationProvider();
                configurationProvider.AddConfigFile("config.json");

                connectionString = configurationProvider.ConnectionString;
            }
            else
            {
                connectionString = $"Filename={location}";
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
