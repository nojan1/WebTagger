using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Jobs;
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

            var jobProcessor = new JobProcessor(tagRepositoryMock.Object, jobRepositoryMock.Object, httpWrapperMock.Object);
            jobProcessor.ProcessJob(new Job
            {
                name = "name",
                url = "URL",
                replace = true,
                selections = new List<Selection>
                {
                    new Selection
                    {
                        output = OutputType.Tag,
                        searchpath = "#templateInfo h2",
                        tagname = "tagname"
                    }
                }
            }).Wait();

            tagRepositoryMock.Verify(x => x.AddOrUpdateTag("URL", "tagname", "&nbsp;<!-- FREE WEBSITE TEMPLATES -->", true));
        }
    }
}
