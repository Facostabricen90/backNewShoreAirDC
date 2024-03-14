using Autofac;
using Autofac.Extensions.DependencyInjection;
using backNewShoreAirDC.IOC;
using BusinessLayer.BusinessDisponibility;
using BusinessLayer.Mapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using DomainLayer.Models.Third;
using Microsoft.EntityFrameworkCore;
using ServicesLayer;
using ServicesLayer.IWebServices;
using ServicesLayer.WebServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);
var OriginsAllow = "_OriginsAllow";
// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutoFacModuleBusiness());
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: OriginsAllow,
                      policy =>
                      {
                          policy.WithOrigins("*"
                                              );
                      });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiNewShoreContext>( options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("APINewShoreAirContext"), b => b.MigrationsAssembly("backNewShoreAirDC"))
);

builder.Services.AddScoped<IDisponibilityBusiness, DisponibilityBusiness>();
builder.Services.AddScoped<IWebService, WebService>();
builder.Services.AddSingleton<IMap<List<Flight>, List<FlightResponse>>, FlightResponse_Journey<List<Flight>, List<FlightResponse>>>();
builder.Services.AddSingleton<IMap<Transport, FlightResponse>, Flight_Transport<Transport, FlightResponse>>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();
app.UseCors(OriginsAllow);
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
