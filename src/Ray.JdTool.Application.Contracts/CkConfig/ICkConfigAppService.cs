using Ray.JdTool.JdCkHistories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Ray.JdTool.CkConfig
{
    public interface ICkConfigAppService : IApplicationService, IRemoteService
    {
        Task<string> GetConfigFileContent();

        Task<CommitResult> CreateUpdateCookie(CreateUpdateJdCkHistoryDto ck);
    }
}
