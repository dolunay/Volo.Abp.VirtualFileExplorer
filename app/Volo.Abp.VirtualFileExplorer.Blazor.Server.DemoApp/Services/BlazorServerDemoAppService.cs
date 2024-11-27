using Volo.Abp.Application.Services;
using Volo.Abp.VirtualFileExplorer.Blazor.Server.DemoApp.Localization;

namespace Volo.Abp.VirtualFileExplorer.Blazor.Server.DemoApp.Services;

/* Inherit your application services from this class. */
public abstract class BlazorServerDemoAppService : ApplicationService
{
    protected BlazorServerDemoAppService()
    {
        LocalizationResource = typeof(BlazorServerDemoAppResource);
    }
}