using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Ray.JdTool
{
    public interface ISignInAppService : IApplicationService, IRemoteService
    {
        void ScanCodeLogin();
    }
}
