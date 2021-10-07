using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ray.JdTool.HealthCheck
{
    public interface IHealthCheckAppService
    {
        Task<bool> GetAsync();
    }
}
