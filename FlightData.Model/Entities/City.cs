namespace FlightData.Model.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public int Population { get; set; }

        public ICollection<Flight> DepartureFlights { get; set; }

        public ICollection<Flight> ArrivalFlights { get; set; }

        public override bool Equals(object? obj)
        {
            return obj != null && ((City)obj)?.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
