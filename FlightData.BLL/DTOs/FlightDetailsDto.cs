using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.BLL.DTOs
{
    public class FlightDetailsDto
    {
        public GetAirlinesDto Airline { get; set; }

        public IEnumerable<GetFlightsDto> Flights { get; set; }

        public TimeSpan? TotalTime { get; set; }
    }
}
