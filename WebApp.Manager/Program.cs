using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WebApp.Manager;
using WebApp.Manager.Store.Territory;
using WebApp.Services.Territory;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddScoped(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var baseUrl = config["Api:ExperienceUrl"] ?? "https://localhost:7263/api/";
    return new HttpClient { BaseAddress = new Uri(baseUrl) };
});

builder.Services.AddMudServices();

builder.Services.AddFluxor(o => 
{
    o.ScanAssemblies(typeof(Program).Assembly);
    o.UseReduxDevTools(); 
});

builder.Services.AddScoped<ITerritoryService, TerritoryService>();

await builder.Build().RunAsync();
