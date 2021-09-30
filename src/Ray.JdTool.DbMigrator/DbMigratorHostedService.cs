using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ray.JdTool.Data;
using Serilog;
using Volo.Abp;

namespace Ray.JdTool.DbMigrator
{
    public class DbMigratorHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DbMigratorHostedService> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime,
            IConfiguration configuration,
            ILogger<DbMigratorHostedService> logger,
            IHostEnvironment hostEnvironment)
        {
            _hostApplicationLifetime = hostApplicationLifetime;
            _configuration = configuration;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var application = AbpApplicationFactory.Create<JdToolDbMigratorModule>(options =>
            {
                options.Services.ReplaceConfiguration(_configuration);
                options.UseAutofac();
                options.Services.AddLogging(c => c.AddSerilog());
            }))
            {
                application.Initialize();

                Log.Logger.Information("当前环境：{env}", _hostEnvironment.EnvironmentName);
                Log.Logger.Information("{conn}", _configuration["ConnectionStrings:Default"]);

                await application
                    .ServiceProvider
                    .GetRequiredService<JdToolDbMigrationService>()
                    .MigrateAsync();

                application.Shutdown();

                _hostApplicationLifetime.StopApplication();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
