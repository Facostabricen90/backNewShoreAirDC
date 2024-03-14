using DomainLayer.Models;

namespace DomainLayer.Contracts
{
    public interface IDisponibilityBusiness
    {
        IEnumerable<Flight> GetDisponibility(Request disponibilityRequest);
    }
}
