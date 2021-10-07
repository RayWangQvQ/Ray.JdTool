using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.FileProviders;
using Ray.JdTool.JdCkTutorials;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.VirtualFileSystem;

namespace Ray.JdTool
{
    public class JdCkTutorialsAppService : JdToolAppService, IJdCkTutorialsAppService
    {
        private const string CACHEKEY = "JdCkTutorial";
        private const string MDFILENAME = "JdCkTutorial.md";

        private readonly IDistributedCache<JdCkTutorialDto> _cache;
        private readonly IWebHostEnvironment _env;

        public JdCkTutorialsAppService(
            IDistributedCache<JdCkTutorialDto> cache,
            IWebHostEnvironment env)
        {
            _cache = cache;
            _env = env;
        }

        public async Task<JdCkTutorialDto> GetAsync()
        {
            return await _cache.GetOrAddAsync(
                CACHEKEY,
                async () => await DoGetAsync(),
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                }
            );
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<JdCkTutorialDto> DoGetAsync()
        {
            string path = Path.Combine(_env.WebRootPath, MDFILENAME);
            var re = await File.ReadAllTextAsync(path);
            return new JdCkTutorialDto
            {
                MarkdownStr = re
            };
        }
    }
}
