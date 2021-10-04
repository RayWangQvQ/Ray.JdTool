using Microsoft.Extensions.Logging;
using Ray.JdTool.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Ray.JdTool.DistributedEventHandlers
{
    public class UserCreatedDistributedEventHandler
        : IDistributedEventHandler<IdentityUserEto>,
          ITransientDependency
    {
        private readonly ILogger<UserCreatedDistributedEventHandler> _logger;

        public UserCreatedDistributedEventHandler(ILogger<UserCreatedDistributedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleEventAsync(IdentityUserEto eventData)
        {
            _logger.LogInformation("接收到mq事件：{uname}(uid)", eventData.UserName, eventData.Id);
            return Task.CompletedTask;
        }
    }
}
