using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Security.Claims;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.Authentication;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.IdentityModel;

namespace Volo.Abp.VirtualFileExplorer.Blazor.WebAssembly.DemoApp;
[Dependency(ReplaceServices = true)]
public class EmptyAuthenticationStateProvider(HttpClient httpClient) : AuthenticationStateProvider
{
    public string? Token { get; private set; }

    private static readonly ClaimsPrincipal anonymousUser = new(new ClaimsIdentity());
    private ClaimsPrincipal currentUser = anonymousUser;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
        => Task.FromResult(new AuthenticationState(currentUser));
}

[Dependency(ReplaceServices = true)]
public class AccessTokenProviderIdentityModelRemoteServiceHttpClientAuthenticator
    : IdentityModelRemoteServiceHttpClientAuthenticator
{
    protected IAbpAccessTokenProvider AccessTokenProvider { get; }

    public AccessTokenProviderIdentityModelRemoteServiceHttpClientAuthenticator(
        IIdentityModelAuthenticationService identityModelAuthenticationService,
        IAbpAccessTokenProvider accessTokenProvider)
        : base(identityModelAuthenticationService)
    {
        AccessTokenProvider = accessTokenProvider;
    }

    public async override Task Authenticate(RemoteServiceHttpClientAuthenticateContext context)
    {
        await Task.CompletedTask;
    }
}

[Dependency(ReplaceServices = true)]
public class WebAssemblyAbpAccessTokenProvider : IAbpAccessTokenProvider, ITransientDependency
{
    public WebAssemblyAbpAccessTokenProvider()
    {
    }

    public virtual async Task<string?> GetTokenAsync()
    {
        return await Task.FromResult("no-token");
    }
}