using Autofac;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebTagger.Configuration;
using WebTagger.Db;
using WebTagger.Jobs;
using WebTagger.Query;
using WebTagger.Webparsing;
using Autofac.Extensions.DependencyInjection;

namespace WebTagger
{
    public class Program
    {
        public static IContainer ApplicationContainer;

        public static void Main(string[] args)
        {
            var arguments = ParseCommandLine(args);

            var configProvider = new ConfigurationProvider();
            foreach (var config in arguments.configurationFiles)
            {
                configProvider.AddConfigFile(config);
            }

            var containerBuilder = SetupIOC(configProvider);

            ConfigureLogging(configProvider);

            if (arguments.background)
            {
                QueryService.Init(configProvider, (services) =>
                {
                    containerBuilder.Populate(services);
                    ApplicationContainer = containerBuilder.Build();

                    return ApplicationContainer;
                });
            }
            else
            {
                ApplicationContainer = containerBuilder.Build();
            }

            ApplicationContainer.Resolve<JobProcessor>().ProcessAllJobs(arguments.background).Wait();
        }

        private static void ConfigureLogging(IConfigurationProvider configProvider)
        {
            if (!string.IsNullOrWhiteSpace(configProvider.LogConfigFilePath))
            {
                XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo(configProvider.LogConfigFilePath));
            }
        }

        private static ContainerBuilder SetupIOC(IConfigurationProvider configurationProvider)
        {
            var builder = new ContainerBuilder();

            builder.Register<IConfigurationProvider>((context) => configurationProvider);
            builder.RegisterType<SqliteContextProvider>().AsImplementedInterfaces();

            builder.RegisterType<JobRepository>().AsImplementedInterfaces();
            builder.RegisterType<TagRepository>().AsImplementedInterfaces();
            builder.RegisterType<SiteRepository>().AsImplementedInterfaces();
            builder.RegisterType<HttpWrapper>().AsImplementedInterfaces();

            builder.RegisterType<JobProcessor>();

            return builder;
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
