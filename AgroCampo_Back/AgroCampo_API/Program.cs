using System;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace ESDAVAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                //.UseSerilog((context, configuration) =>
                //{
                //    configuration.Enrich.FromLogContext()
                //    .Enrich.WithExceptionDetails()
                //      .Enrich.WithMachineName()
                //      .WriteTo.Debug()
                //      .WriteTo.Console()
                //      .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticConfiguration:Uri"]))
                //      {
                //          AutoRegisterTemplate = true,
                //          IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
                //      })
                //      .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                //      .ReadFrom.Configuration(context.Configuration);
                //})
                ;
    }
}
