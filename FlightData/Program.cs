using FlightData.BLL;
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
var flightPlannerService = host.Services.GetRequiredService<IFlightPlannerService>();

var seeder = new Seed(dbContext);
seeder.SeedDatabase();

var smallestCity = await cityServices.GetSmallestCityAsync();
var biggestCity = await cityServices.GetBiggestCityAsync();

var routesByAirlines = await flightPlannerService.GetShortestRouteBetweenCitiesAsync(smallestCity.Id, biggestCity.Id, false);

Console.WriteLine(Environment.NewLine);
Console.WriteLine($"Legkisebb város: {smallestCity.Name}, {smallestCity.Population} lakos");
Console.WriteLine($"Legnagyobb város: {biggestCity.Name}, {biggestCity.Population} lakos");
Console.WriteLine(Environment.NewLine);

Console.WriteLine("A legrövidebb út:");

foreach (var route in routesByAirlines)
{
    Console.WriteLine($"\t{route.Airline.Name}");
    if (route.Flights.Any())
    {

        foreach (var flight in route.Flights)
        {
            Console.WriteLine($"\t {flight.StartCity.Name} -> {flight.DestinationCity.Name}: {TimeHelpers.CalculateDifference(flight.TakeOffDate, flight.ArrivalDate).ToHourMinuteFormat()}");
        }
        Console.WriteLine("\t Összesen: " + route.TotalTime);
    }
    else
    {
        Console.WriteLine("\t Nincs útvonal");
    }
}

var result = await flightPlannerService.GetShortestRouteBetweenCitiesAsync(smallestCity.Id, biggestCity.Id, true);

var routeByAnyAirline = result.FirstOrDefault();
Console.WriteLine(Environment.NewLine);

if (routeByAnyAirline != null)
{
    Console.WriteLine($"Bármely légitársasággal a legrövidebb út:");
    if (routeByAnyAirline.Flights.Any())
    {
        foreach (var flight in routeByAnyAirline.Flights)
        {
            Console.WriteLine($"\t {flight.Airline.Name} {flight.StartCity.Name} -> {flight.DestinationCity.Name}: {TimeHelpers.CalculateDifference(flight.TakeOffDate, flight.ArrivalDate).ToHourMinuteFormat()}");
        }
        Console.WriteLine("\t Összesen: " + routeByAnyAirline.TotalTime);
    }
    else
    {
        Console.WriteLine("\t Nincs útvonal");
    }
}

Console.ReadKey();