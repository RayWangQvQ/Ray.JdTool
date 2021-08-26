using Ray.JdTool.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Ray.JdTool.Blazor
{
    public abstract class JdToolComponentBase : AbpComponentBase
    {
        protected JdToolComponentBase()
        {
            LocalizationResource = typeof(JdToolResource);
        }
    }
}
