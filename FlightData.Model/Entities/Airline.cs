namespace FlightData.Model.Entities
{
    public class Airline : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Flight> Flights { get; set; }
    }
}
