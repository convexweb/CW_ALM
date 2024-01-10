using CW_ALM.Client;
using CW_ALM.Client.Common;
using CW_ALM.Client.Helpers;
using CW_ALM.Client.Services;
using CW_ALM.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped<IAuthenticationService, AuthenticationService>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IHttpService, HttpService>()
    .AddScoped<ILocalStorageService, LocalStorageService>();


// configure http client
builder.Services.AddScoped(x => {
    var apiUrl = new Uri(builder.Configuration["apiUrl"]);

    // use fake backend if "fakeBackend" is "true" in appsettings.json
    if (builder.Configuration["fakeBackend"] == "true")
        return new HttpClient(new FakeBackendHandler()) { BaseAddress = apiUrl };

    return new HttpClient() { BaseAddress = apiUrl };
});
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var host = builder.Build();

await host.SetDefaultCulture();

var authenticationService = host.Services.GetRequiredService<IAuthenticationService>();
await authenticationService.Initialize();


await host.RunAsync();