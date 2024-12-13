using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface IProductRepository
{   
    // Merchandise
    List<Product> Get(int pageNumber, int pageSize, int sellerId);
    // Shop
    List<Product> Get(int pageNumber, int pageSize, string name, string categoryName, string description);
    Product GetById(int id);
    int Count();
    int Count(string name, string categoryName, string description);
    void Insert(Product product);
    void Update(Product product);
    void Delete(Product product);
}
