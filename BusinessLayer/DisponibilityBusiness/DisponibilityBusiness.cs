using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using DomainLayer.Models.Third;
using Newtonsoft.Json;
using NLog;
using ServicesLayer.IWebServices;
using System;

namespace BusinessLayer.BusinessDisponibility
{
    public class DisponibilityBusiness : IDisponibilityBusiness
    {
        private readonly IWebService _webService;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMap<List<Flight>, List<FlightResponse>> _map;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public DisponibilityBusiness(
            IWebService service,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IMapper mapper,
            IMap<List<Flight>, List<FlightResponse>> map
            )
        {
            _webService = service;
            _configuration = configuration;
            _mapper = mapper;
            _map = map;
        }

        public IEnumerable<Flight> GetDisponibility(Request disponibilityRequest)
        {
            IEnumerable<Flight> flights = new List<Flight>();
            string? url = _configuration.GetSection("AppSettings").GetSection("URLExternalS").Value;
            try
            {
                string result = _webService.GetHTTPService(url);
                var flightsResponse = JsonConvert.DeserializeObject<List<FlightResponse>>(result);
                if (!(flightsResponse is null) && flightsResponse.Count() > (int)default)
                {
                    flights = _map.Map(flightsResponse);
                    var route = FindRoute(flights, disponibilityRequest?.Origin?.ToUpper(), disponibilityRequest?.Destination?.ToUpper());
                    return route;
                }
            }
            catch (Exception e)
            {
                logger.Info($"Error while trying send Request with Server API ::Exception {JsonConvert.SerializeObject(e)} ::URL {JsonConvert.SerializeObject(disponibilityRequest)}");
            }
            return flights;
        }

        static List<Flight> FindRoute(IEnumerable<Flight> flights, string origin, string destination)
        {
            Queue<List<Flight>> queue = new Queue<List<Flight>>();
            HashSet<string> visited = new HashSet<string>();

            queue.Enqueue(new List<Flight>());

            while (queue.Count > 0)
            {
                List<Flight> currentPath = queue.Dequeue();

                if (currentPath.Count > 0)
                    visited.Add(currentPath.Last().Destination);

                if (currentPath.Count > 0 && currentPath.Last().Destination == destination)
                {
                    return currentPath;
                }

                List<Flight> nextFlights = flights
                    .Where(f => f.Origin == (currentPath.Count > 0 ? currentPath.Last().Destination : origin))
                    .ToList();

                foreach (Flight nextFlight in nextFlights)
                {
                    if (!visited.Contains(nextFlight.Destination))
                    {
                        visited.Add(nextFlight.Destination);

                        List<Flight> newPath = new List<Flight>(currentPath);
                        newPath.Add(nextFlight);

                        queue.Enqueue(newPath);
                    }
                }
            }
            return new List<Flight>();
        }
    }
}
