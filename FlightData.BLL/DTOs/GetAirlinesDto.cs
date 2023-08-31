namespace FlightData.BLL.DTOs
{
    public class GetAirlinesDto
    {
        public string Name { get; set; }

        public ICollection<GetFlightsDto> Flights { get; set; }
    }
}
