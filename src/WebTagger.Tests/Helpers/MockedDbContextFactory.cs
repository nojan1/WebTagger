using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WebTagger.Db;

namespace WebTagger.Tests.Helpers
{
    public class MockedDbContextFactory
    {
        public static Mock<IDbContextProvider> GetMock([CallerMemberName]string dbName = "")
        {
            var dbContextProviderMock = new Mock<IDbContextProvider>();
            dbContextProviderMock.Setup<ApplicationContext>(x => x.GetContext())
                .Returns(() =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                    optionsBuilder.UseInMemoryDatabase(dbName);

                    return new ApplicationContext(optionsBuilder.Options);
                });

            return dbContextProviderMock;
        }
    }
}
