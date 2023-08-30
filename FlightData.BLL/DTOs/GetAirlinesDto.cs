using FlightData.Model.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.BLL.DTOs
{
    public class GetAirlinesDto
    {
        public string Name { get; set; }
        public ICollection<GetFlightsDto> Flights { get; set; }
    }
}
