using System.Globalization;
using TrollMarketBusiness.Interfaces;
using TrollMarketWeb.ViewModels.Profile;

namespace TrollMarketWeb.Services;

public class ProfileService
{
    private readonly IAccountRepository _repository;

    public ProfileService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public ProfileViewModel GetProfile(string username)
    {
        var model = _repository.Get(username);
        return new ProfileViewModel()
        {
            Username = model.Username,
            Name = model.Name,
            Role = model.Role,
            Address = model.Address,
            Balance = model.Balance?.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"))
        };
    }

    public HistoryProfileIndexViewModel Get(string username)
    {
        var model = _repository.GetOrders(username)
        .Select(order => new HistoryProfileViewModel() {
            OrderDate = order.OrderDate,
            Product = order.Product.Name,
            Qty = order.Qty,
            Shipment = order.Shipment.Name,
            TotalPrice = ((order.Product.Price * order.Qty) + order.Shipment.Cost).ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"))
        });

        return new HistoryProfileIndexViewModel()
        {
            Histories = model.ToList(),
            Profile = GetProfile(username)
        };
    }
}
