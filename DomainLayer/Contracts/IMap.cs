
namespace DomainLayer.Contracts
{
    public interface IMap<A, B>
    {
        A Map(B origin);
    }
}
