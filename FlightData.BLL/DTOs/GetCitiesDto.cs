namespace FlightData.BLL.DTOs
{
    public class GetCitiesDto
    {
        public int CityId { get; set; }

        public string Name { get; set; }

        public int Population { get; set; }

        public IEnumerable<GetFlightsDto> DepartureFlights { get; set; }
        public IEnumerable<GetFlightsDto> ArrivalFlights { get; set; }
    }
}
