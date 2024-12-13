using System.Globalization;
using TrollMarketBusiness.Interfaces;

namespace TrollMarketAPI.Merchandise;

public class MerchandiseService
{
    private readonly IProductRepository _productRepository;

    public MerchandiseService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public MerchandiseInfoDTO GetInfo(int id)
    {
        var model = _productRepository.GetById(id);

        return new MerchandiseInfoDTO()
        {
            Id = model.Id,
            Name = model.Name,
            CategoryName = model.CategoryName,
            Description = model.Description,
            Price = model.Price.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID")),
            Discontinue = model.Discontinue == true ? "Yes" : "No"
        };
    }
}
