using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syrna.Alpha.Blazor.Host;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var application = await builder.AddApplicationAsync<DemoAppModule>(options =>
{
    options.UseAutofac();
});

var host = builder.Build();
await application.InitializeApplicationAsync(host.Services);
await host.RunAsync();
