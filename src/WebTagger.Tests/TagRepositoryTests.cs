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

            tagRepository.AddTag("URL", "NAME", "VALUE", 1);

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

            tagRepository.AddTag("URL", "NAME", "VALUE", 1);
            tagRepository.AddTag("URL", "NAME", "VALUE2", 1);
            tagRepository.AddTag("URL", "NAME", "VALUE3", 1);
            tagRepository.AddTag("URL", "NAME", "VALUE4", 1);

            tagRepository.Clear("URL");

            using (var context = dbContextProvider.Object.GetContext())
            {
                Assert.Equal(1, context.Sites.Count());
                Assert.Equal(0, context.Tags.Count());
            }
        }

        [Fact]
        public void ListingTagsRespectsAccessLevel()
        {
            var dbContextProvider = MockedDbContextFactory.GetMock();

            var tagRepository = new TagRepository(dbContextProvider.Object);

            tagRepository.AddTag("URL", "NAME", "VALUE", 1);
            tagRepository.AddTag("URL", "NAME", "VALUE2", 2);
            tagRepository.AddTag("URL2", "NAME", "VALUE3", 3);
            tagRepository.AddTag("URL2", "NAME", "VALUE4", 4);

            var tags = tagRepository.List(3);
            Assert.Equal(2, tags.Count);
            Assert.True(tags.Any(t => t.Value == "VALUE") &&
                        tags.Any(t => t.Value == "VALUE2"));
        }

        [Fact]
        public void SearchinTagsRespectsAccessLevel()
        {
            var dbContextProvider = MockedDbContextFactory.GetMock();

            var tagRepository = new TagRepository(dbContextProvider.Object);

            tagRepository.AddTag("URL", "NAME", "VALUE", 1);
            tagRepository.AddTag("URL", "NAME", "VALUE2", 2);
            tagRepository.AddTag("URL2", "NAME", "VALUE3", 3);
            tagRepository.AddTag("URL2", "NAME", "VALUE4", 4);

            var tags = tagRepository.SearchTags("a", 3);
            Assert.Equal(2, tags.Count);
            Assert.True(tags.Any(t => t.Value == "VALUE") &&
                        tags.Any(t => t.Value == "VALUE2"));
        }
    }
}
