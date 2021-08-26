using Ray.JdTool.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ray.JdTool.Permissions
{
    public class JdToolPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(JdToolPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(JdToolPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<JdToolResource>(name);
        }
    }
}
