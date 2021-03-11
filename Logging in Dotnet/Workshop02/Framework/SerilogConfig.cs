using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;

namespace Framework
{
    public static class SerilogConfig
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
           (hostingContext, loggerConfiguration) =>
           {

               var env = hostingContext.HostingEnvironment;
               var applicationName = env.ApplicationName;
               var environmentName = env.EnvironmentName;
               var indexFormat = hostingContext.Configuration["IndexFormat"];


               loggerConfiguration.MinimumLevel.Information()
                   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                   .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Warning)
                   .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                    .Enrich.WithProperty("ApplicationName", applicationName)
                   .Enrich.WithProperty("EnvironmentName", environmentName)
                   .Enrich.With<ActivityEnricher>()
                   .WriteTo.Console();

               if (hostingContext.HostingEnvironment.IsDevelopment())
               {
                   loggerConfiguration.MinimumLevel.Override("Workshop02", LogEventLevel.Debug);
               }

               var elasticUrl = "http://localhost:9200";

               if (!string.IsNullOrEmpty(elasticUrl))
               {
                   loggerConfiguration.WriteTo.Elasticsearch(
                       new ElasticsearchSinkOptions(new Uri(elasticUrl))
                       {
                           AutoRegisterTemplate = true,
                           AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                           IndexFormat = indexFormat,
                           MinimumLogEventLevel = LogEventLevel.Debug
                       });
               }

               var seq = "http://localhost:5341";

               if (!string.IsNullOrEmpty(seq))
               {
                   loggerConfiguration.WriteTo.Seq(seq);
               }
           };
    }
}
