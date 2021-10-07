using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Ray.JdTool.Localization;
using Volo.Abp.Application.Services;

namespace Ray.JdTool
{
    /* Inherit your application services from this class.
     */
    public abstract class JdToolAppService : ApplicationService
    {
        protected JdToolAppService()
        {
            LocalizationResource = typeof(JdToolResource);
        }
    }
}
