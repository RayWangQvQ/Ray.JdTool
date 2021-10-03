using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Ray.JdTool.Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        IConfiguration Configuration { get; set; }

        string LogoUrl { get; set; }

        protected async override Task OnInitializedAsync()
        {
            LogoUrl = Path.Combine(Configuration["RemoteServices:Default:BaseUrl"], "logo.png");

            await base.OnInitializedAsync();
        }
    }
}
