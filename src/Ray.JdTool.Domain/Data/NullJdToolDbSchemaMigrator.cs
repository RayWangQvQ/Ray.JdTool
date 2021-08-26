using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ray.JdTool.Data
{
    /* This is used if database provider does't define
     * IJdToolDbSchemaMigrator implementation.
     */
    public class NullJdToolDbSchemaMigrator : IJdToolDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}