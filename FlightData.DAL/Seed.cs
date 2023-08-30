using FlightData.Model.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.DAL
{
    public class Seed
    {
        private readonly FlightDataContext _dbContext;

        public Seed(FlightDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedDatabase()
        {
            SeedInit();
        }

        private void SeedInit()
        {
            var cityBudapest = new City
            {
                Name = "Budapest",
                Population = 1756000
            };

            var cityMilan = new City
            {
                Name = "Milan",
                Population = 1371498
            };

            var cityCorfu = new City
            {
                Name = "Corfu",
                Population = 1756000
            };

            var cityDenpasar = new City
            {
                Name = "Denpasar",
                Population = 897300
            };

            var cityPorto = new City
            {
                Name = "Porto",
                Population = 214349
            };

            _dbContext.Cities.AddRange(cityBudapest, cityMilan, cityCorfu, cityDenpasar, cityPorto);
            var airlines = new List<Airline>
                {
                    new Airline
                    {
                        Name ="WizzAir",
                        Flights = new List<Flight>()
                        {
                            new Flight { 
                                TakeOffDate = DateTimeOffset.Parse("2023.11.02 14:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.02 16:00"),
                                StartCity = cityBudapest,
                                DestinationCity = cityPorto,
                                Distance = 2295.92,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.10.06 18:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.10.06 17:00"),
                                StartCity = cityBudapest,
                                DestinationCity = cityMilan,
                                Distance = 786.45,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.04 12:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.04 14:00"),
                                StartCity = cityBudapest,
                                DestinationCity = cityCorfu,
                                Distance = 878.81,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.02 14:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.02 16:00"),
                                StartCity = cityBudapest,
                                DestinationCity = cityPorto,
                                Distance = 2295.92,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.11 15:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.11 16:00"),
                                StartCity = cityCorfu,
                                DestinationCity = cityBudapest,
                                Distance = 878.81,
                            },                            
                        }
                    },
                    new Airline
                    {
                        Name ="Qatar Airways",
                        Flights = new List<Flight>()
                        {
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.02 14:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.03 13:30"),
                                StartCity = cityBudapest,
                                DestinationCity = cityDenpasar,
                                Distance = 11178.37,
                            },
                           new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.10.14 18:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.10.15 17:30"),
                                StartCity = cityDenpasar,
                                DestinationCity = cityMilan,
                                Distance = 11939,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.04 12:35"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.04 13:57"),
                                StartCity = cityPorto,
                                DestinationCity = cityMilan,
                                Distance = 1514,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.11 14:45"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.11 16:15"),
                                StartCity = cityCorfu,
                                DestinationCity = cityBudapest,
                                Distance = 878.81,
                            },
                        }
                    },
                };

            _dbContext.Airlines.AddRange(airlines);
            _dbContext.SaveChanges();
        }
    }
}
