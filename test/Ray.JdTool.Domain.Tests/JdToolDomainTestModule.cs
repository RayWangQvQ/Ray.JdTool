using Ray.JdTool.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Ray.JdTool
{
    [DependsOn(
        typeof(JdToolEntityFrameworkCoreTestModule)
        )]
    public class JdToolDomainTestModule : AbpModule
    {

    }
}