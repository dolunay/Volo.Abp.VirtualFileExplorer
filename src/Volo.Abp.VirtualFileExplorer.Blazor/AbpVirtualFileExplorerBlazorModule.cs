using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileExplorer.Blazor.Localization;
using Volo.Abp.VirtualFileExplorer.Blazor.Navigation;
using Volo.Abp.VirtualFileExplorer.Blazor.Services;
using Volo.Abp.VirtualFileSystem;

namespace Volo.Abp.VirtualFileExplorer.Blazor;

[DependsOn(
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class AbpVirtualFileExplorerBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new VirtualFileExplorerMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AbpVirtualFileExplorerBlazorModule>("Volo.Abp.VirtualFileExplorer.Blazor");
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<VirtualFileExplorerResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/Resources");
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(AbpVirtualFileExplorerBlazorModule).Assembly);
        });

        context.Services.AddScoped<IVirtualFileExplorerAppService, VirtualFileExplorerAppService>();
    }
}
