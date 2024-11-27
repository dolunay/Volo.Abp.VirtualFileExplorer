﻿using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileExplorer.Blazor.WebAssembly.DemoApp;
using Volo.Abp.VirtualFileExplorer.Blazor.WebAssembly;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.UI.Navigation;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Volo.Abp.VirtualFileExplorer.Blazor.WebAssembly.DemoApp.Menus;
using Volo.Abp.Localization;
using Volo.Abp.AspNetCore.Components.WebAssembly.BasicTheme;
using Volo.Abp.AspNetCore.Components.Web.BasicTheme.Themes.Basic;
using Volo.Abp.Http.Client;

namespace Syrna.Alpha.Blazor.Host;

[DependsOn(typeof(AbpAutoMapperModule))]
[DependsOn(typeof(AbpAutofacWebAssemblyModule))]
[DependsOn(typeof(AbpAspNetCoreComponentsWebAssemblyBasicThemeModule))]
[DependsOn(typeof(AbpVirtualFileExplorerBlazorWebAssemblyModule))]
[DependsOn(typeof(AbpHttpClientModule))]

public class DemoAppModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<AbpAspNetCoreComponentsWebOptions>(options =>
        {
            options.IsBlazorWebApp = false;
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
        var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

        //ConfigureAuthentication(builder);
        ConfigureHttpClient(context, environment);
        ConfigureAuthorization(builder);
        ConfigureBlazorise(context);
        ConfigureRouter(context);
        ConfigureUi(builder);
        ConfigureMenu(context);
        ConfigureAutoMapper(context);

        context.Services.AddAutoMapperObjectMapper<DemoAppModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<DemoAppAutoMapperProfile>(validate: true);
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("el", "el", "Ελληνικά"));
        });
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(DemoAppModule).Assembly;
        });
    }

    private static void ConfigureAuthorization(WebAssemblyHostBuilder builder)
    {
        //builder.Services.AddAuthorizationCore();
        //builder.Services.AddScoped<AuthenticationStateProvider, EmptyAuthenticationStateProvider>();
        //builder.Services.AddAlwaysAllowAuthorization();

        //builder.Services.RemoveAll<AccessTokenProviderIdentityModelRemoteServiceHttpClientAuthenticator>();
        //builder.Services.RemoveAll<AuthenticationStateProvider>();
        //builder.Services.AddScoped<AuthenticationStateProvider, EmptyAuthenticationStateProvider>();
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new DemoAppMenuContributor(context.Services.GetConfiguration()));
        });
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }

    private static void ConfigureUi(WebAssemblyHostBuilder builder)
    {
        builder.RootComponents.Add<AppWithoutAuth>("#ApplicationContainer");
    }

    private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
    {
        context.Services.AddTransient(sp => new HttpClient
        {
            BaseAddress = new Uri(environment.BaseAddress)
        });
    }

    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<DemoAppModule>();
        });
    }
    //public override void OnApplicationInitialization(ApplicationInitializationContext context)
    //{
    //    var app = context.GetApplicationBuilder();

    //    app.MapAbpStaticAssets();
    //    app.UseRouting();
    //    app.UseAbpRequestLocalization();
    //    app.UseConfiguredEndpoints();
    //}
}