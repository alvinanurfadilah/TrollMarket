using System.Globalization;
using TrollMarketBusiness.Interfaces;
using TrollMarketWeb.ViewModels;
using TrollMarketWeb.ViewModels.Profile;
using TrollMarketWeb.ViewModels.Shop;
using static TrollMarketWeb.ViewModels.Constant;

namespace TrollMarketWeb.Services;

public class ShopService
{
    private readonly IProductRepository _productRepository;
    private readonly IAccountRepository _accountRepository;

    public ShopService(IProductRepository productRepositoryrepository, IAccountRepository accountRepository)
    {
        _productRepository = productRepositoryrepository;
        _accountRepository = accountRepository;
    }

    public ProfileViewModel GetId(string username)
    {
        var model = _accountRepository.Get(username);

        return new ProfileViewModel()
        {
            Id = model.Id
        };
    }

    public ShopIndexViewModel Get(int pageNumber, string name, string categoryName, string description)
    {
        var model = _productRepository.Get(pageNumber, PageSize, name, categoryName, description)
        .Select(product => new ShopViewModel() {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID")),
            Discontinue = product.Discontinue
        });

        return new ShopIndexViewModel()
        {
            Shops = model.ToList(),
            Pagination = new PaginationViewModel() {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _productRepository.Count(name, categoryName, description)
            },
            Name = name,
            CategoryName = categoryName,
            Description = description
        };
    }
}
