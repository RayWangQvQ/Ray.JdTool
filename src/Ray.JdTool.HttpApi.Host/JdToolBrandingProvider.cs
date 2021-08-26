using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Ray.JdTool
{
    [Dependency(ReplaceServices = true)]
    public class JdToolBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "JdTool";
    }
}
