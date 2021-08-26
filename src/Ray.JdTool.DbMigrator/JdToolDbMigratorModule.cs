using Ray.JdTool.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Ray.JdTool.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(JdToolEntityFrameworkCoreModule),
        typeof(JdToolApplicationContractsModule)
        )]
    public class JdToolDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
