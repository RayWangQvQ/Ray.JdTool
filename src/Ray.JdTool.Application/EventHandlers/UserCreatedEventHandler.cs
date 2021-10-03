using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Identity;
using System.Text.Json;
using Volo.Abp.PermissionManagement;
using Ray.JdTool.Permissions;

namespace Ray.JdTool.EventHandlers
{
    public class UserCreatedEventHandler : ILocalEventHandler<EntityCreatedEventData<IdentityUser>>,
          ITransientDependency
    {
        private readonly ILogger<UserCreatedEventHandler> _logger;
        private readonly IPermissionManager _permissionManager;

        public UserCreatedEventHandler(
            ILogger<UserCreatedEventHandler> logger,
            IPermissionManager permissionManager)
        {
            _logger = logger;
            _permissionManager = permissionManager;
        }

        public async Task HandleEventAsync(EntityCreatedEventData<IdentityUser> eventData)
        {
            _logger.LogInformation("监听到新用户注册事件：{uname}({uid})", eventData.Entity.UserName, eventData.Entity.Id);

            await _permissionManager.SetForUserAsync(eventData.Entity.Id, JdToolPermissions.CommitCookie, true);

            _logger.LogInformation("已自动为该新增用户({uid})赋值提交Cookie权限", eventData.Entity.Id);
        }
    }
}
