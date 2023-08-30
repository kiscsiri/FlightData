using FlightData.BLL.Interfaces;
using FlightData.BLL.MapperProfiles;
using FlightData.BLL.Services;
using FlightData.DAL;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<FlightDataContext>();
builder.Services.AddAutoMapper(typeof(FlightDataProfile));
builder.Services.AddTransient<IAirlineServices, AirlineServices>();
builder.Services.AddTransient<ICityServices, CityServices>();
builder.Services.AddTransient<IFlightServices, FlightService>();
builder.Services.AddTransient<IFlightPlannerService, FlightPlannerService>();

using IHost host = builder.Build();
host.RunAsync();

var dbContext = host.Services.GetRequiredService<FlightDataContext>();
var cityServices = host.Services.GetRequiredService<ICityServices>();
var airlineServices = host.Services.GetRequiredService<IAirlineServices>();
var flightServices = host.Services.GetRequiredService<IFlightServices>();
var flightPlannerService = host.Services.GetRequiredService<IFlightServices>();

var seeder = new Seed(dbContext);
seeder.SeedDatabase();

var smallestCity = await cityServices.GetSmallestCityAsync();
var biggestCity = await cityServices.GetBiggestCityAsync();

Console.WriteLine($"Legkisebb város: {smallestCity.Name}, {smallestCity.Population} lakos");
Console.WriteLine($"Legnagyobb város: {biggestCity.Name}, {biggestCity.Population} lakos");