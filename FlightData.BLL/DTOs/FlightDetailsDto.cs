using FlightData.Model.Entities;

namespace FlightData.BLL.DTOs
{
    public class FlightDetailsDto
    {
        public Airline Airline { get; set; }

        public IList<Flight> Flights { get; set; } = new List<Flight>();

        public string TotalTime
        {
            get
            {
                if (Flights.Any())
                {
                    return TimeHelpers.CalculateDifference(Flights.First().TakeOffDate, Flights.Last().ArrivalDate).ToHourMinuteFormat();
                }

                return "";
            }
        }

        public double TotalDistance
        {
            get
            {
                return Flights.Sum(f => f.Distance);
            }
        }
    }
}
