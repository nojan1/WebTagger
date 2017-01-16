using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Jobs;
using WebTagger.Jobs.Configuration;
using WebTagger.Webparsing;

namespace WebTagger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var job = new Job
            {
                Name = "test",
                Url = "http://galaxen.com/restaurang/dagens-lunch/",
                Selections = new List<Selection>
                {
                    new Selection
                    {
                        Output = OutputType.Tag,
                        Hardcoded = false,
                        TagName = "lunch",
                        SearchPath = ".container .lunch-list li ul li"
                    },
                    new Selection
                    {
                        Output = OutputType.Tag,
                        Hardcoded = false,
                        TagName = "week",
                        SearchPath = @".container h2 r:Matsedel&nbsp;(\w+)"
                    }
                }
            };

            var arguments = ParseCommandLine(args);
            var container = SetupIOC();

            var configProvider = container.Resolve<IConfigurationProvider>();
            foreach(var config in arguments.configurationFiles)
            {
                configProvider.AddConfigFile(config);
            }

            container.Resolve<JobProcessor>().ProcessAllJobs(arguments.background);
        }

        private static IContainer SetupIOC()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConfigurationProvider>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<JobRepository>().AsImplementedInterfaces();
            builder.RegisterType<TagRepository>().AsImplementedInterfaces();
            builder.RegisterType<HttpWrapper>().AsImplementedInterfaces();

            builder.RegisterType<JobProcessor>();

            return builder.Build();
        }

        private static CommandLineArguments ParseCommandLine(string[] args)
        {
            var commandLineArgumets = new CommandLineArguments();

            var parser = new CommandLineParser.CommandLineParser();
            parser.ExtractArgumentAttributes(commandLineArgumets);
            parser.ParseCommandLine(args);

            return commandLineArgumets;
        }
    }
}
