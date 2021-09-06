using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using Volo.Abp.AspNetCore.Mvc;

namespace Ray.JdTool.Controllers
{
    public class HomeController : AbpController
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActionResult Index()
        {
            var homePage = _configuration["App:ClientUrl"];
            if (homePage.IsNullOrEmpty())
            {
                return Redirect("~/swagger");
            }
            else
            {
                return Redirect(homePage);
            }
        }
    }
}
