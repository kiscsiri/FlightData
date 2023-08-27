using FlightData.DAL;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTransient<FlightDataContext>();

using IHost host = builder.Build();
host.RunAsync();

var dbContext = host.Services.GetRequiredService<FlightDataContext>();

var seeder = new Seed(dbContext);
seeder.SeedDatabase();

Console.WriteLine("Hello FlightRadar!");