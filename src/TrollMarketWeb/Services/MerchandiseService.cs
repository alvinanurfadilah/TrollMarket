using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels;
using TrollMarketWeb.ViewModels.Merchandise;
using static TrollMarketWeb.ViewModels.Constant;

namespace TrollMarketWeb.Services;

public class MerchandiseService
{
    private readonly IProductRepository _productRepository;
    private readonly IAccountRepository _accountRepository;

    public MerchandiseService(IProductRepository productRepository, IAccountRepository accountRepository)
    {
        _productRepository = productRepository;
        _accountRepository = accountRepository;
    }

    public int Get(string username)
    {
        var model = _accountRepository.Get(username);
        return model.Id;
    }

    public MerchandiseIndexViewModel Get(int pageNumber, int sellerId)
    {
        var model = _productRepository.Get(pageNumber, PageSize, sellerId)
        .Select(product => new MerchandiseViewModel() {
            Id = product.Id,
            Name = product.Name,
            CategoryName = product.CategoryName,
            Discontinue = product.Discontinue == true ? "Yes" : "No"
        });

        return new MerchandiseIndexViewModel()
        {
            Merchandises = model.ToList(),
            Pagination = new PaginationViewModel() {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _productRepository.Count()
            },
        };
    }

    public MerchandiseFormViewModel GetById(int id)
    {
        var model = _productRepository.GetById(id);

        return new MerchandiseFormViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            CategoryName = model.CategoryName,
            Description = model.Description,
            Price = model.Price,
            Discontinue = (bool)model.Discontinue,
            AccountId = model.AccountId
        };
    }

    public void Insert(MerchandiseFormViewModel viewModel)
    {
        var model = new Product()
        {
            Name = viewModel.Name,
            CategoryName = viewModel.CategoryName,
            Description = viewModel.Description,
            Price = viewModel.Price,
            Discontinue = viewModel.Discontinue,
            AccountId = viewModel.AccountId
        };

        _productRepository.Insert(model);
    }

    public void Update(MerchandiseFormViewModel viewModel)
    {
        var model = new Product()
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            CategoryName = viewModel.CategoryName,
            Description = viewModel.Description,
            Price = viewModel.Price,
            Discontinue = viewModel.Discontinue,
            AccountId = viewModel.AccountId
        };

        _productRepository.Update(model);
    }

    public void Discontinue(int id)
    {
        var model = _productRepository.GetById(id);
        model.Discontinue = true;
        _productRepository.Update(model);
    }

    public void Delete(int id)
    {
        var model = _productRepository.GetById(id);
        _productRepository.Delete(model);
    }
}
