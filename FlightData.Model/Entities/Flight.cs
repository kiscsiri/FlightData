namespace FlightData.Model.Entities
{
    public class Flight : BaseEntity
    {
        public double Distance { get; set; }

        public DateTimeOffset ArrivalDate { get; set; }

        public DateTimeOffset TakeOffDate { get; set; }

        public City DestinationCity { get; set; }
        public int DestinationCityId { get; set; }

        public City StartCity { get; set; }
        public int StartCityId { get; set; }

        public Airline Airline { get; set; }
        public int AirlineId { get; set; }
    }
}
