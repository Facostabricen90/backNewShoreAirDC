using DomainLayer.Contracts;
using DomainLayer.Models.Third;
using DomainLayer.Models;

namespace BusinessLayer.Mapper
{
    public class Flight_Transport<A, B> : IMap<A, B>
    {
        A IMap<A, B>.Map(B origin)
        {
            var transport = new Transport();

            FlightResponse flight = (FlightResponse)Convert.ChangeType(origin, typeof(FlightResponse));

            transport.FlightCarrier = flight.FlightCarrier;
            transport.FlightNumber = flight.FlightNumber;
            return (A)(object)transport;
        }
    }
}
