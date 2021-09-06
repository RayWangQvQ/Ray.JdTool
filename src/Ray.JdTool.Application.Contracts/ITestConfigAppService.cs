using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Ray.JdTool
{
    public interface ITestConfigAppService : IApplicationService, IRemoteService
    {
        string Test(string configKey);
    }
}
