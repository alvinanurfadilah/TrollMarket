using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly TrollMarketContext _dbContext;

    public ProductRepository(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Product> Get(int pageNumber, int pageSize, int sellerId)
    {
        return _dbContext.Products
        .Include(product => product.Account)
        .Where(product => product.Account.Id == sellerId)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }
    public List<Product> Get(int pageNumber, int pageSize, string name, string categoryName, string description)
    {
        return _dbContext.Products
        .Where(product => product.Name.ToLower().Contains(name??"".ToLower()) && product.CategoryName.ToLower().Contains(categoryName??"".ToLower()) && product.Description.ToLower().Contains(description??"".ToLower()))
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public int Count(string name, string categoryName, string description)
    {
        return _dbContext.Products
        .Where(product => product.Name.ToLower().Contains(name??"".ToLower()) && product.CategoryName.ToLower().Contains(categoryName??"".ToLower()) && product.Description.ToLower().Contains(description??"".ToLower()))
        .Count();
    }
    public int Count()
    {
        return _dbContext.Products
        .Count();
    }

    public Product GetById(int id)
    {
        return _dbContext.Products
        .Include(product => product.Account)
        .Where(product => product.Id == id)
        .FirstOrDefault();
    }

    public void Insert(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product);
        _dbContext.SaveChanges();
    }

    public void Delete(Product product)
    {
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();
    }
}
