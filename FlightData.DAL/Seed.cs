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
                Id = 1,
                Name = "Budapest",
                Population = 1756000
            };

            var cityMilan = new City
            {
                Id = 2,
                Name = "Milan",
                Population = 1371498
            };

            var cityCorfu = new City
            {
                Id = 3,
                Name = "Corfu",
                Population = 1756000
            };

            var cityDenpasar = new City
            {
                Id = 4,
                Name = "Denpasar",
                Population = 897300
            };

            var cityPorto = new City
            {
                Id = 5,
                Name = "Porto",
                Population = 214349
            };

            _dbContext.Cities.AddRange(cityBudapest, cityMilan, cityCorfu, cityDenpasar, cityPorto);
            var airlines = new List<Airline>
                {
                    new Airline
                    {
                        Id = 1,
                        Name ="Wizz Air",
                        Flights = new List<Flight>()
                        {
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.02 14:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.02 16:00"),
                                StartCity = cityBudapest,
                                DestinationCity = cityPorto,
                                Distance = 2295.92,
                                AirlineId = 1,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.10.08 14:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.10.08 16:00"),
                                StartCity = cityMilan,
                                DestinationCity = cityBudapest,
                                Distance = 2295.92,
                                AirlineId = 1,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.10.07 18:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.10.07 19:30"),
                                StartCity = cityPorto,
                                DestinationCity = cityMilan,
                                Distance = 786.45,
                                AirlineId = 1,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.04 12:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.04 14:00"),
                                StartCity = cityBudapest,
                                DestinationCity = cityCorfu,
                                Distance = 878.81,
                                AirlineId = 1,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.02 14:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.02 16:00"),
                                StartCity = cityBudapest,
                                DestinationCity = cityPorto,
                                Distance = 2295.92,
                                AirlineId = 1,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.11 15:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.11 16:00"),
                                StartCity = cityCorfu,
                                DestinationCity = cityBudapest,
                                Distance = 878.81,
                                AirlineId = 1,
                            },
                        }
                    },
                    new Airline
                    {
                        Id = 2,
                        Name ="Qatar Airways",
                        Flights = new List<Flight>()
                        {
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.02 14:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.03 13:30"),
                                StartCity = cityBudapest,
                                DestinationCity = cityDenpasar,
                                Distance = 11178.37,
                                AirlineId = 2,
                            },
                           new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.10.14 18:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.10.15 17:30"),
                                StartCity = cityDenpasar,
                                DestinationCity = cityMilan,
                                Distance = 11939,
                                AirlineId = 2,
                           },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.04 12:35"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.04 13:57"),
                                StartCity = cityPorto,
                                DestinationCity = cityMilan,
                                Distance = 1514,
                                AirlineId = 2,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.11 14:45"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.11 16:15"),
                                StartCity = cityCorfu,
                                DestinationCity = cityBudapest,
                                Distance = 878.81,
                                AirlineId = 2,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.10.07 22:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.10.07 23:30"),
                                StartCity = cityMilan,
                                DestinationCity = cityBudapest,
                                Distance = 400.92,
                                AirlineId = 2,
                            },
                        }
                    },
                    new Airline
                    {
                        Id = 3,
                        Name ="Ryanair",
                        Flights = new List<Flight>()
                        {
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.11.02 14:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.11.03 13:30"),
                                StartCity = cityBudapest,
                                DestinationCity = cityDenpasar,
                                Distance = 11178.37,
                                AirlineId = 3,
                            },
                            new Flight {
                                TakeOffDate = DateTimeOffset.Parse("2023.10.14 18:00"),
                                ArrivalDate = DateTimeOffset.Parse("2023.10.15 17:30"),
                                StartCity = cityDenpasar,
                                DestinationCity = cityMilan,
                                Distance = 11939,
                                AirlineId = 3,
                            },
                        }
                    },
                };

            _dbContext.Airlines.AddRange(airlines);
            _dbContext.SaveChanges();
        }
    }
}
