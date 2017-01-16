using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Jobs;
using WebTagger.Jobs.Configuration;
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

            tagRepositoryMock.Verify(x => x.AddOrUpdateTag("URL", "tagname", "&nbsp;<!-- FREE WEBSITE TEMPLATES -->", true));
        }
    }
}
