using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileExplorer.Blazor.Server.DemoApp.Localization;

namespace Volo.Abp.VirtualFileExplorer.Blazor.Server.DemoApp.Menus;

public class BlazorServerDemoAppMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<BlazorServerDemoAppResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                BlazorServerDemoAppMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home",
                order: 0
            )
        );

        //if (BlazorServerDemoAppModule.IsMultiTenant)
        //{
        //    administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        //}
        //else
        //{
        //    administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        //}

        return Task.CompletedTask;
    }
}
