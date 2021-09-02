using Ray.JdTool.JdCkHistories;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Ray.JdTool.CkConfig
{
    public interface ICkConfigAppService : IApplicationService, IRemoteService
    {
        string GetConfigFileContent();

        string CreateUpdateCookie(CreateUpdateJdCkHistoryDto ck);
    }
}
