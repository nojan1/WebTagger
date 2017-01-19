using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Configuration;

namespace WebTagger.Query
{
    public static class QueryService
    {
        public static void Init(IConfigurationProvider configurationProvider, Func<IServiceCollection, IContainer> registerServices)
        {
            Startup.RegisterServices = registerServices;

            var host = new WebHostBuilder()
               .UseKestrel()
               .UseUrls(configurationProvider.QueryServiceListenUrl)
               .UseContentRoot(Directory.GetCurrentDirectory())
               .UseStartup<Startup>()
               .Build();

            host.Start();
        }
    }
}
