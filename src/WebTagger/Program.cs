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
using WebTagger.Webparsing;

namespace WebTagger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var arguments = ParseCommandLine(args);
            var container = SetupIOC();

            var configProvider = container.Resolve<IConfigurationProvider>();
            foreach(var config in arguments.configurationFiles)
            {
                configProvider.AddConfigFile(config);
            }

            ConfigureLogging(configProvider);

            container.Resolve<JobProcessor>().ProcessAllJobs(arguments.background).Wait();
        }

        private static void ConfigureLogging(IConfigurationProvider configProvider)
        {
            if (!string.IsNullOrWhiteSpace(configProvider.LogConfigFilePath))
            {
                XmlConfigurator.Configure(LogManager.GetRepository(Assembly.GetEntryAssembly()), new FileInfo(configProvider.LogConfigFilePath));
            }
        }

        private static IContainer SetupIOC()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConfigurationProvider>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<SqliteContextProvider>().AsImplementedInterfaces();

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
