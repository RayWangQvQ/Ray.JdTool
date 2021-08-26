using Volo.Abp.Modularity;

namespace Ray.JdTool
{
    [DependsOn(
        typeof(JdToolApplicationModule),
        typeof(JdToolDomainTestModule)
        )]
    public class JdToolApplicationTestModule : AbpModule
    {

    }
}