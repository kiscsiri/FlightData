using FlightData.Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.BLL.DTOs
{
    public class GetFlightsDto
    {
        public double Distance { get; set; }

        public DateTimeOffset ArrivalDate { get; set; }

        public DateTimeOffset TakeOffDate { get; set; }

        public GetCitiesDto DestinationCity { get; set; }

        public GetCitiesDto StartCity { get; set; }

        public GetAirlinesDto Airline { get; set; }
    }
}
