using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Ray.JdTool.JdCkHistories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Ray.JdTool
{
    public class JdCkHistoriesAppService : CrudAppService<JdCkHistory, JdCkHistoryDto, Guid,
            PagedAndSortedResultRequestDto, CreateUpdateJdCkHistoryDto>,
        IJdCkHistoriesAppService
    {
        private readonly IRepository<JdCkHistory, Guid> _repository;
        private readonly IWebHostEnvironment _env;

        public JdCkHistoriesAppService(IRepository<JdCkHistory, Guid> repository,
            IWebHostEnvironment env) : base(repository)
        {
            _repository = repository;
            _env = env;
        }
    }
}
