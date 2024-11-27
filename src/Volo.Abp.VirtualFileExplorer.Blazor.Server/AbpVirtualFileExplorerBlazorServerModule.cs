using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Volo.Abp.VirtualFileExplorer.Blazor.Server;

[DependsOn(
    typeof(AbpVirtualFileExplorerBlazorModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
)]
public class AbpVirtualFileExplorerBlazorServerModule : AbpModule
{
}
