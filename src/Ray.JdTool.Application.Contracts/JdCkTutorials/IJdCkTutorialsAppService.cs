using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Ray.JdTool.JdCkTutorials
{
    public interface IJdCkTutorialsAppService : IApplicationService
    {
        Task<JdCkTutorialDto> GetAsync();

        Task UpdateAsync();
    }
}
