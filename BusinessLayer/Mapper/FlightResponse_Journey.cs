using DomainLayer.Contracts;
using DomainLayer.Models.Third;
using DomainLayer.Models;

namespace BusinessLayer.Mapper
{
    public class FlightResponse_Journey<A, B> : IMap<A, B>
    {
        private readonly IMap<Transport, FlightResponse> _map;

        public FlightResponse_Journey(IMap<Transport, FlightResponse> map)
        {
            _map = map;
        }
        public A Map(B origin)
        {
            var flightResponse = new List<Flight>();

            List<FlightResponse> flights = (List<FlightResponse>)Convert.ChangeType(origin, typeof(List<FlightResponse>));
            foreach (var flight in flights)
            {
                var flightAux = new Flight();
                flightAux.Origin = flight.Origin;
                flightAux.Destination = flight.Destination;
                flightAux.Price = flight.Price;
                flightAux.Transport = _map.Map(flight);
                flightResponse.Add(flightAux);
            }

            return (A)(Object)flightResponse;
        }
    }
}
