using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface IOrderRepository
{
    // History Buyer
    List<Order> Get(string username);
    // History Admin
    List<Order> Get(int pageNumber, int pageSize, int buyer, int seller);
    // List<Order> GetHistorySeller(int id);
    Order AddQty(int productId, int shipmentId, int accountId);
    Order GetById(int id);
    int Count(int buyer, int seller);
    void Insert(Order order);
    void Update(Order order);
    void Delete(Order order);
}