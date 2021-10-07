using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Ray.JdTool
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var seqServer = Environment.GetEnvironmentVariable("SeqServerUrl");
            var loggerConfiguration = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
#if DEBUG
                .WriteTo.Async(c => c.Console())
#endif
                .WriteTo.Async(c => c.File("Logs/logs.txt", rollingInterval: RollingInterval.Day));
            if (!seqServer.IsNullOrWhiteSpace())
            {
                loggerConfiguration.WriteTo.Async(c => c.Seq(seqServer));
            }
            Log.Logger = loggerConfiguration.CreateLogger();

            try
            {
                Log.Information("Starting Ray.JdTool.HttpApi.Host.");
                Log.Information("Seq server url is:{url}", Environment.GetEnvironmentVariable("SeqServerUrl"));
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(build =>
                {
                    build.AddJsonFile("appsettings.secrets.json", optional: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac()
                .UseSerilog();
    }
}
