using CW_ALM.Fluent;
using CW_ALM.Fluent.Common;
using CW_ALM.Fluent.Common.Interfaces;
using CW_ALM.Fluent.Helpers;
using CW_ALM.Fluent.Services;
using CW_ALM.Fluent.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddFluentUIComponents();

builder.Services.RegisterServices(builder.Configuration);

builder.Services
    .AddScoped<IJsonSerializerExtension, JsonSerializerExtension>()
    .AddScoped<ServerValidator>()
    .AddScoped<AuthenticationStateProvider>(sp => (AuthenticationStateProvider)sp.GetRequiredService<IAuthStateProvider>())
    .AddAuthorizationCore();

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

var authenticationService = host.Services.GetRequiredService<IAuthStateProvider>();
await authenticationService.Initialize();


await host.RunAsync();
