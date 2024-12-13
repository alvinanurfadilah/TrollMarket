using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI.Carts;

public class CartService
{
    private readonly IOrderRepository _repository;
    private readonly IAccountRepository _accountRepository;
    private readonly IShipmentRepository _shipmentRepository;
    private readonly IProductRepository _productRepository;

    public CartService(IOrderRepository repository, IAccountRepository accountRepository, IShipmentRepository shipmentRepository, IProductRepository productRepository)
    {
        _repository = repository;
        _accountRepository = accountRepository;
        _shipmentRepository = shipmentRepository;
        _productRepository = productRepository;
    }

    public DetailInfoDTO GetDetail(int id)
    {
        var model = _productRepository.GetById(id);

        return new DetailInfoDTO()
        {
            Id = model.Id,
            Name = model.Name,
            CategoryName = model.CategoryName,
            Description = model.Description,
            Price = model.Price.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID")),
            SellerName = model.Account.Name
        };
    }

    private List<SelectListItem> GetShipment()
    {
        var model = _shipmentRepository.Get()
        .Where(shipment => shipment.IsService == true)
        .Select(shipment => new SelectListItem() {
            Text = shipment.Name,
            Value = shipment.Id.ToString()
        }).ToList();

        return model;
    }

    public CartFormDTO Get()
    {
        return new CartFormDTO()
        {
            Shipments = GetShipment()
        };
    }

    public int Get(string username)
    {
        var model = _accountRepository.GetUsername(username);
        return model.Id;
    }

    public void Insert(CartFormDTO dto)
    {
        var existingOrder = _repository.AddQty(dto.ProductId, dto.ShipmentId, dto.AccountId);

        if (existingOrder != null)
        {
            existingOrder.Qty += dto.Qty;
            _repository.Update(existingOrder);
        } 
        else
        {
            var model = new Order()
            {
                ProductId = dto.ProductId,
                Qty = dto.Qty,
                AccountId = dto.AccountId,
                ShipmentId = dto.ShipmentId
            };

            _repository.Insert(model);
        }
    }
}
