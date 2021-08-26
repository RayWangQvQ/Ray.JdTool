using Ray.JdTool.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ray.JdTool.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class JdToolController : AbpController
    {
        protected JdToolController()
        {
            LocalizationResource = typeof(JdToolResource);
        }
    }
}