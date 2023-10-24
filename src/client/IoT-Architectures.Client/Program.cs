using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using IoT_Architectures.Client;
using IoT_Architectures.Client.Core;
using IoT_Architectures.Client.Core.Rest.Endpoints;
using IoT_Architectures.Client.Core.Rest;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.RegisterCore();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<IRestClient, RestClient>();
builder.Services.AddTransient<TemperateRecordsRestClient>();

await builder.Build().RunAsync();