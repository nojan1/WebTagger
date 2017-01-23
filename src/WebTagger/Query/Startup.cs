﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Query.Helpers;

namespace WebTagger.Query
{
    public class Startup
    {
        public static Func<IServiceCollection, IContainer> RegisterServices;

        public Startup(IHostingEnvironment env) { }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            var container = RegisterServices?.Invoke(services);
            if (container != null)
            {
                return new AutofacServiceProvider(container);
            }
            else
            {
                return new DefaultServiceProviderFactory().CreateServiceProvider(services);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Console.WriteLine("sdsadsad");
            loggerFactory.AddLog4Net();

            app.UseMvc();
        }
    }
}
