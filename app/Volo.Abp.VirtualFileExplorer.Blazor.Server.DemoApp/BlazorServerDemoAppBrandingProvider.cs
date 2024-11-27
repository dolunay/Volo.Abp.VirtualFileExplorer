using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;
using Volo.Abp.VirtualFileExplorer.Blazor.Server.DemoApp.Localization;

namespace Volo.Abp.VirtualFileExplorer.Blazor.Server.DemoApp;

[Dependency(ReplaceServices = true)]
public class BlazorServerDemoAppBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<BlazorServerDemoAppResource> _localizer;

    public BlazorServerDemoAppBrandingProvider(IStringLocalizer<BlazorServerDemoAppResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
