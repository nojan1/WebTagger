using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Jobs;
using WebTagger.Webparsing;

namespace WebTagger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var job = new Job
            {
                name = "test",
                url = "http://galaxen.com/restaurang/dagens-lunch/",
                selections = new List<Selection>
                {
                    new Selection
                    {
                        output = OutputType.Tag,
                        hardcoded = false,
                        tagname = "lunch",
                        searchpath = ".container .lunch-list li ul li"
                    },
                    new Selection
                    {
                        output = OutputType.Tag,
                        hardcoded = false,
                        tagname = "week",
                        searchpath = @".container h2 r:Matsedel&nbsp;(\w+)"
                    }
                }
            };

            var container = SetupIOC();
            container.Resolve<JobProcessor>().ProcessJob(job).Wait();
        }

        private static IContainer SetupIOC()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<JobRepository>().AsImplementedInterfaces();
            builder.RegisterType<TagRepository>().AsImplementedInterfaces();
            builder.RegisterType<HttpWrapper>().AsImplementedInterfaces();

            builder.RegisterType<JobProcessor>();

            return builder.Build();
        }
    }
}
