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
            var configurationProvider = new ConfigurationProvider();
            configurationProvider.AddConfigFile("config.json");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlite(configurationProvider.ConnectionString);

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
