using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface IShipmentRepository
{
    List<Shipment> Get();
    List<Shipment> Get(int pageNumber, int pageSize);
    Shipment GetById(int id);
    int Count();
    void Insert(Shipment shipment);
    void Update(Shipment shipment);
    void Delete(Shipment shipment);
}
