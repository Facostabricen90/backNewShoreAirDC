
using System.Text.Json.Serialization;

namespace DomainLayer.Models.ApiData
{
    public class DataApiFlights
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public double Price { get; set; }
        public string FlightCarrier { get; set; }
        public string FlightNumber { get; set; }
    }
}
