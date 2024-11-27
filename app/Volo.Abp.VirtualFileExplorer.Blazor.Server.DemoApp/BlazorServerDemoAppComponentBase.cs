using Volo.Abp.AspNetCore.Components;
using Volo.Abp.VirtualFileExplorer.Blazor.Server.DemoApp.Localization;

namespace Volo.Abp.VirtualFileExplorer.Blazor.Server.DemoApp;

public abstract class BlazorServerDemoAppComponentBase : AbpComponentBase
{
    protected BlazorServerDemoAppComponentBase()
    {
        LocalizationResource = typeof(BlazorServerDemoAppResource);
    }
}
