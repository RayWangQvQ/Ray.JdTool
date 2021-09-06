using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace Ray.JdTool
{
    public class TestConfigAppService : JdToolAppService, ITestConfigAppService
    {
        private readonly IConfiguration _configuration;

        public TestConfigAppService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Test(string configKey)
        {
            return _configuration[configKey];
        }
    }
}
