using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ray.JdTool.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace Ray.JdTool.Blazor.Menus
{
    public class JdToolMenuContributor : IMenuContributor
    {
        private readonly IConfiguration _configuration;

        public JdToolMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
            else if (context.Menu.Name == StandardMenus.User)
            {
                await ConfigureUserMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<JdToolResource>();

            context.Menu.Items.Insert(
                0,
                new ApplicationMenuItem(
                    JdToolMenus.Home,
                    l["Menu:Home"],
                    "/",
                    icon: "fas fa-home"
                )
            );

            context.Menu.Items.Insert(
                1,
                new ApplicationMenuItem(
                    JdToolMenus.CommitCookie,
                    l["Menu:Commit"],
                    "/commit-cookie",
                    icon: "fas fa-cookie-bite"
                    )
                );

            return Task.CompletedTask;
        }

        private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            var accountStringLocalizer = context.GetLocalizer<AccountResource>();

            var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

            context.Menu.AddItem(new ApplicationMenuItem(
                "Account.Manage",
                accountStringLocalizer["MyAccount"],
                $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
                icon: "fa fa-cog",
                order: 1000,
                null).RequireAuthenticated());

            return Task.CompletedTask;
        }
    }
}
