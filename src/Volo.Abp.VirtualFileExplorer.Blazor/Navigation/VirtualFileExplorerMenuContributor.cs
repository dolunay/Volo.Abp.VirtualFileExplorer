using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileExplorer.Blazor.Localization;

namespace Volo.Abp.VirtualFileExplorer.Blazor.Navigation;

public class VirtualFileExplorerMenuContributor : IMenuContributor
{
    public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var l = context.GetLocalizer<VirtualFileExplorerResource>();

        context.Menu.Items.Add(new ApplicationMenuItem(VirtualFileExplorerMenuNames.Index, l["Menu:VirtualFileExplorer"], icon: "fa fa-file", url: "~/VirtualFileExplorer"));

        return Task.CompletedTask;
    }
}
