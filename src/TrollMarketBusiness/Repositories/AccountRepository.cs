using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Exceptions;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly TrollMarketContext _dbContext;

    public AccountRepository(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Account> Get()
    {
        return _dbContext.Accounts.ToList();
    }

    public Account Get(string username)
    {
        return _dbContext.Accounts
        .Include(account => account.Orders)
        .ThenInclude(account => account.Product)
        .ThenInclude(product => product.Account)
        .Include(account => account.Orders)
        .ThenInclude(account => account.Shipment)
        .Where(account => account.Username == username)
        .FirstOrDefault() ?? throw new LoginException("Username or Password or Role is invalid!");
    }

    public List<Order> GetOrders(string username)
    {
        return _dbContext.Orders
        .Include(account => account.Product)
        .ThenInclude(product => product.Account)
        .Include(account => account.Shipment)
        .Where(order => order.Account.Username == username || order.Product.Account.Username == username)
        .ToList();
    }

    public Account GetUsername(string username)
    {
        return _dbContext.Accounts
        .Where(account => account.Username == username)
        .FirstOrDefault();
    }

    public Account GetSeller(int accountId)
    {
        return _dbContext.Accounts
        .Include(account => account.Orders)
        .ThenInclude(account => account.Product)
        .ThenInclude(product => product.Account)
        .Where(account => account.Id == accountId)
        .FirstOrDefault();
    }

    public void Insert(Account account)
    {
        _dbContext.Accounts.Add(account);
        _dbContext.SaveChanges();
    }

    public void Update(Account account)
    {
        _dbContext.Update(account);
        _dbContext.SaveChanges();
    }
}
