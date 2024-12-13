using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface IAccountRepository
{
    List<Account> Get();
    Account Get(string username);
    Account GetUsername(string username);
    Account GetSeller(int accountId);
    void Insert(Account account);
    void Update(Account account);
    List<Order> GetOrders(string username);
}
