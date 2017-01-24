using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Configuration;
using WebTagger.Db;
using WebTagger.Jobs;
using WebTagger.Tests.Helpers;
using WebTagger.Webparsing;
using Xunit;

namespace WebTagger.Tests
{
    public class JobProcessorTests
    {
        [Fact]
        public void TagsGetsAddedToRepository()
        {
            var httpWrapperMock = new Mock<IHttpWrapper>();
            httpWrapperMock.Setup(x => x.GetPageContent(It.IsAny<string>()))
                           .Returns(Task.FromResult(StaticWebPageContent.Html));

            var jobRepositoryMock = new Mock<IJobRepository>();

            var tagRepositoryMock = new Mock<ITagRepository>();

            var configurationMock = new Mock<IConfigurationProvider>();

            var jobProcessor = new JobProcessor(tagRepositoryMock.Object, jobRepositoryMock.Object, httpWrapperMock.Object, configurationMock.Object);
            jobProcessor.ProcessJob(new Job
            {
                Name = "name",
                Url = "URL",
                Replace = true,
                Selections = new List<Selection>
                {
                    new Selection
                    {
                        Output = OutputType.Tag,
                        SearchPath = "#templateInfo h2",
                        TagName = "tagname"
                    }
                }
            }).Wait();

            tagRepositoryMock.Verify(x => x.AddTag("URL", "tagname", "&nbsp;<!-- FREE WEBSITE TEMPLATES -->", 1));
        }

        [Fact]
        public void HardcodedValuesGetsAddedToRepository()
        {
            var httpWrapperMock = new Mock<IHttpWrapper>();
            httpWrapperMock.Setup(x => x.GetPageContent(It.IsAny<string>()))
                           .Returns(Task.FromResult(StaticWebPageContent.Html));

            var jobRepositoryMock = new Mock<IJobRepository>();

            var tagRepositoryMock = new Mock<ITagRepository>();

            var configurationMock = new Mock<IConfigurationProvider>();

            var jobProcessor = new JobProcessor(tagRepositoryMock.Object, jobRepositoryMock.Object, httpWrapperMock.Object, configurationMock.Object);
            jobProcessor.ProcessJob(new Job
            {
                Name = "name",
                Url = "URL",
                Replace = true,
                Selections = new List<Selection>
                {
                    new Selection
                    {
                        Output = OutputType.Tag,
                        Hardcoded = true,
                        TagName = "tagname",
                        Value = "HardCodedValue"
                    }
                }
            }).Wait();

            tagRepositoryMock.Verify(x => x.AddTag("URL", "tagname", "HardCodedValue", 1));
        }

        [Fact]
        public void AdHocJobsGetsCreatedFromUrls()
        {
            var httpWrapperMock = new Mock<IHttpWrapper>();
            httpWrapperMock.Setup(x => x.GetPageContent(It.IsAny<string>()))
                           .Returns(Task.FromResult(StaticWebPageContent.Html));

            var callCount = 0;
            var jobRepositoryMock = new Mock<IJobRepository>();
            jobRepositoryMock.Setup(x => x.RegisterAdhocJob(It.IsAny<string>(), "whatever"))
                             .Callback<string, string>((x1, x2) => { callCount++; });

            var tagRepositoryMock = new Mock<ITagRepository>();

            var configurationMock = new Mock<IConfigurationProvider>();

            var jobProcessor = new JobProcessor(tagRepositoryMock.Object, jobRepositoryMock.Object, httpWrapperMock.Object, configurationMock.Object);
            jobProcessor.ProcessJob(new Job
            {
                Name = "name",
                Url = "URL",
                Replace = true,
                Selections = new List<Selection>
                {
                    new Selection
                    {
                        Output = OutputType.Url,
                        SearchPath = "#navigation a a:href",
                        JobName = "whatever"
                    }
                }
            }).Wait();

            Assert.Equal(5, callCount);
        }

        [Fact]
        public void JobCreatesTagInDatabase()
        {
            var job = new Job
            {
                Name = "name",
                Url = "URL",
                Replace = true,
                Selections = new List<Selection>
                {
                    new Selection
                    {
                        Output = OutputType.Tag,
                        SearchPath = "#templateInfo h2",
                        TagName = "tagname"
                    }
                }
            };

            var httpWrapperMock = new Mock<IHttpWrapper>();
            httpWrapperMock.Setup(x => x.GetPageContent(It.IsAny<string>()))
                           .Returns(Task.FromResult(StaticWebPageContent.Html));

            var jobRepositoryMock = new Mock<IJobRepository>();

            var dbContextProvider = MockedDbContextFactory.GetMock();
            var tagRepository = new TagRepository(dbContextProvider.Object);

            var configurationMock = new Mock<IConfigurationProvider>();

            var jobProcessor = new JobProcessor(tagRepository, jobRepositoryMock.Object, httpWrapperMock.Object, configurationMock.Object);

            jobProcessor.ProcessJob(job).Wait();

            using (var context = dbContextProvider.Object.GetContext())
            {
                Assert.Equal(1, context.Sites.Count());
                Assert.Equal(1, context.Tags.Count());
            }
        }

        [Fact]
        public void ReplaceSettingWorksCorrectlyForTags()
        {
            var job = new Job
            {
                Name = "name",
                Url = "URL",
                Replace = true,
                Selections = new List<Selection>
                {
                    new Selection
                    {
                        Output = OutputType.Tag,
                        SearchPath = "#templateInfo h2",
                        TagName = "tagname"
                    }
                }
            };

            var httpWrapperMock = new Mock<IHttpWrapper>();
            httpWrapperMock.Setup(x => x.GetPageContent(It.IsAny<string>()))
                           .Returns(Task.FromResult(StaticWebPageContent.Html));

            var jobRepositoryMock = new Mock<IJobRepository>();

            var dbContextProvider = MockedDbContextFactory.GetMock();
            var tagRepository = new TagRepository(dbContextProvider.Object);

            var configurationMock = new Mock<IConfigurationProvider>();

            var jobProcessor = new JobProcessor(tagRepository, jobRepositoryMock.Object, httpWrapperMock.Object, configurationMock.Object);

            jobProcessor.ProcessJob(job).Wait();

            int tagId;
            using (var context = dbContextProvider.Object.GetContext())
            {
                tagId = context.Tags.First().Id;
            }

            tagRepository.Clear("URL");

            jobProcessor.ProcessJob(job).Wait();

            using (var context = dbContextProvider.Object.GetContext())
            {
                Assert.Equal(1, context.Sites.Count());
                Assert.Equal(1, context.Tags.Count());
                Assert.True(context.Tags.First().Id > tagId);
            }
        }
    }
}
