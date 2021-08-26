using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ray.JdTool.Data;
using Volo.Abp.DependencyInjection;

namespace Ray.JdTool.EntityFrameworkCore
{
    public class EntityFrameworkCoreJdToolDbSchemaMigrator
        : IJdToolDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreJdToolDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the JdToolDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<JdToolDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
