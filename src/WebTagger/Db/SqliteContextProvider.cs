using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Configuration;

namespace WebTagger.Db
{
    public class SqliteContextProvider : IDbContextProvider
    {
        private readonly IConfigurationProvider configurationProvider;

        public SqliteContextProvider(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
        }

        public ApplicationContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlite(configurationProvider.ConnectionString);

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
