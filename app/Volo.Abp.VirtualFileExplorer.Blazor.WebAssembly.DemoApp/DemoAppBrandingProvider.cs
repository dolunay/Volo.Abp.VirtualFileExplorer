using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Volo.Abp.VirtualFileExplorer.Blazor.WebAssembly.DemoApp;

[Dependency(ReplaceServices = true)]
public class DemoAppBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DemoApp";
    public override string LogoUrl => "logo.svg";
}
