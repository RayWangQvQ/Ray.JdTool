using Volo.Abp.Settings;

namespace Ray.JdTool.Settings
{
    public class JdToolSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(JdToolSettings.MySetting1));
        }
    }
}
