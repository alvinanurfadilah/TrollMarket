using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class ShipmentRepository : IShipmentRepository
{
    private readonly TrollMarketContext _dbContext;

    public ShipmentRepository(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Shipment> Get()
    {
        return _dbContext.Shipments.ToList();
    }

    public List<Shipment> Get(int pageNumber, int pageSize)
    {
        return _dbContext.Shipments
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public int Count()
    {
        return _dbContext.Shipments.Count();
    }

    public Shipment GetById(int id)
    {
        return _dbContext.Shipments.Find(id);
    }

    public void Insert(Shipment shipment)
    {
        _dbContext.Shipments.Add(shipment);
        _dbContext.SaveChanges();
    }

    public void Update(Shipment shipment)
    {
        _dbContext.Shipments.Update(shipment);
        _dbContext.SaveChanges();
    }

    public void Delete(Shipment shipment)
    {
        _dbContext.Remove(shipment);
        _dbContext.SaveChanges();
    }
}
