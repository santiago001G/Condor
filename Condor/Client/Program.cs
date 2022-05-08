using Condor.Client;
using Condor.Client.Helpers;
using Condor.Client.Repositorio;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tewr.Blazor.FileReader;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<SpinnerService>();
builder.Services.AddSweetAlert2(options => {
    options.Theme = SweetAlertTheme.Bulma;
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IRepositorio, Repositorio>();
builder.Services.AddFileReaderService(options => { 
    options.UseWasmSharedBuffer = true;
});

await builder.Build().RunAsync();
