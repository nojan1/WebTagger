using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Webparsing;
using Xunit;

namespace WebTagger.Tests
{
    public class PageparserTests
    {
        [Fact]
        public void CSSSelectorSelectionReturnsCorrectData()
        {
            var pageParser = new Pageparser(StaticWebPageContent.Html);
            var data = pageParser.GetSelectionValues("#templateInfo h2");

            Assert.NotEmpty(data);
            Assert.Contains("FREE WEBSITE", data[0]);
        }

        [Fact]
        public void AttributeSelectionReturnsCorrectData()
        {
            var pageParser = new Pageparser(StaticWebPageContent.Html);
            var data = pageParser.GetSelectionValues("#navigation a a:href");

            Assert.Equal(5, data.Count);
        }

        [Fact]
        public void RegexSelectionReturnsCorrectdata()
        {
            var pageParser = new Pageparser(StaticWebPageContent.Html);
            var data = pageParser.GetSelectionValues(@"#featured h2 r:\s(\w{2})\s");

            Assert.Equal("to", data[0]);
        }
    }
}
