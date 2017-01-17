using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Db;
using WebTagger.Tests.Helpers;
using Xunit;

namespace WebTagger.Tests
{
    public class TagRepositoryTests
    {
        [Fact]
        public void AddingTagCreatesTagAndSiteInDatabase()
        {
            var dbContextProvider = MockedDbContextFactory.GetMock();

            var tagRepository = new TagRepository(dbContextProvider.Object);

            tagRepository.AddTag("URL", "NAME", "VALUE");

            using(var context = dbContextProvider.Object.GetContext())
            {
                Assert.Equal(1, context.Sites.Count());
                Assert.Equal(1, context.Tags.Count());

                Assert.Equal("URL", context.Sites.First().Url);
                Assert.Equal("NAME", context.Tags.First().Name);
                Assert.Equal("VALUE", context.Tags.First().Value);
                Assert.Equal("URL", context.Tags.Include(x => x.Site).First().Site.Url);
            }
        }

        [Fact]
        public void TagsAreClearedCorrectly()
        {
            var dbContextProvider = MockedDbContextFactory.GetMock();

            var tagRepository = new TagRepository(dbContextProvider.Object);

            tagRepository.AddTag("URL", "NAME", "VALUE");
            tagRepository.AddTag("URL", "NAME", "VALUE2");
            tagRepository.AddTag("URL", "NAME", "VALUE3");
            tagRepository.AddTag("URL", "NAME", "VALUE4");

            tagRepository.Clear("URL");

            using (var context = dbContextProvider.Object.GetContext())
            {
                Assert.Equal(1, context.Sites.Count());
                Assert.Equal(0, context.Tags.Count());
            }
        }
    }
}
