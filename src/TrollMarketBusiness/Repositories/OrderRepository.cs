using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly TrollMarketContext _dbContext;

    public OrderRepository(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Cart
    public List<Order> Get(string username)
    {
        return _dbContext.Orders
        .Include(order => order.Account)
        .Where(order => order.OrderDate == null && order.Account.Username == username)
        .ToList();
    }

    // History
    public List<Order> Get(int pageNumber, int pageSize, int buyer, int seller)
    {
        return _dbContext.Orders
        .Where(order => (order.AccountId == buyer || 0 == buyer) && (order.Product.Account.Id == seller || 0 == seller) && order.OrderDate != null)
        .Include(order => order.Account)
        .Include(order => order.Product)
        .ThenInclude(order => order.Account)
        .Include(order => order.Shipment)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public int Count(int buyer, int seller)
    {
        return _dbContext.Orders
        .Where(order => (order.AccountId == buyer || 0 == buyer) && (order.Product.Account.Id == seller || 0 == seller) && order.OrderDate != null)
        .Count();
    }

    public Order GetById(int id)
    {
        return _dbContext.Orders.Find(id);
    }

    public void Insert(Order order)
    {
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }

    public void Update(Order order)
    {
        _dbContext.Orders.Update(order);
        _dbContext.SaveChanges();
    }

    public void Delete(Order order)
    {
        _dbContext.Orders.Remove(order);
        _dbContext.SaveChanges();
    }

    public Order AddQty(int productId, int shipmentId, int accountId)
    {
        return _dbContext.Orders
        .Where(order => order.Product.Id == productId && order.Shipment.Id  == shipmentId && order.Account.Id == accountId && order.OrderDate == null)
        .Include(order => order.Account)
        .Include(order => order.Product)
        .ThenInclude(order => order.Account)
        .Include(order => order.Shipment)
        .FirstOrDefault();
    }
}
