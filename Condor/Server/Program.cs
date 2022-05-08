using Condor.Core.Interfaces;
using Condor.Core.IService;
using Condor.Core.Service;
using Condor.Infraestructure.Persistence.Data;
using Condor.Infraestructure.Persistence.Repositories;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRazorPages();

#region Dependency Inyection

builder.Services.AddDbContext<CondorContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("CondorDbContextConection"))
);

builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IAbonosClienteRepository, AbonosClienteRepository>();
builder.Services.AddTransient<IProductosClienteRepository, ProductosClienteRepository>();
builder.Services.AddTransient<IMercaderiaRepository, MercaderiaRepository>();

builder.Services.AddTransient<IClienteService, ClienteService>();
builder.Services.AddTransient<ICobroService, CobroService>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
