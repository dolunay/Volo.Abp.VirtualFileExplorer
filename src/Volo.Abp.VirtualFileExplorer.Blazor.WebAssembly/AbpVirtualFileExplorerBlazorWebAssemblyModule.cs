using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileExplorer.Blazor;

namespace Volo.Abp.VirtualFileExplorer.Blazor.WebAssembly;

[DependsOn(
    typeof(AbpVirtualFileExplorerBlazorModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
)]
public class AbpVirtualFileExplorerBlazorWebAssemblyModule : AbpModule
{
}
