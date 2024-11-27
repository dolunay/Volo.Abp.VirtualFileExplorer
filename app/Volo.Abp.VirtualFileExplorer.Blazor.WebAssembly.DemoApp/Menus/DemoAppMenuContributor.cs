using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileExplorer.Blazor.Localization;

namespace Volo.Abp.VirtualFileExplorer.Blazor.WebAssembly.DemoApp.Menus;

public class DemoAppMenuContributor(IConfiguration configuration) : IMenuContributor
{
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
        var l = context.GetLocalizer<VirtualFileExplorerResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                DemoAppMenus.Home,
                l["Menu:Home"],
                "/",
                icon: Blazorise.Icons.FontAwesome.FontAwesomeIcons.Home
            )
        );

        return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<VirtualFileExplorerResource>();

        var identityServerUrl = configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null)/*.RequireAuthenticated()*/);

        return Task.CompletedTask;
    }
}
