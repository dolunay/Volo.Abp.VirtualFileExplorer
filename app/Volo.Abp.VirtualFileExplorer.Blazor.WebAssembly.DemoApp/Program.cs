using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Volo.Abp.VirtualFileExplorer.Blazor.WebAssembly.DemoApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var application = await builder.AddApplicationAsync<DemoAppModule>(options =>
{
    options.UseAutofac();
});

var host = builder.Build();
await application.InitializeApplicationAsync(host.Services);
await host.RunAsync();
