using Microsoft.Extensions.Logging;
using Ray.JdTool.HealthCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.JdTool
{
    public class HealthCheckAppService : JdToolAppService, IHealthCheckAppService
    {
        private readonly ILogger<HealthCheckAppService> _logger;

        public HealthCheckAppService(ILogger<HealthCheckAppService> logger)
        {
            _logger = logger;
        }

        public Task<bool> GetAsync()
        {
            _logger.LogInformation("This is a Health Check");
            return Task.FromResult(true);
        }
    }
}
