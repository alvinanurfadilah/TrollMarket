using System.Globalization;
using TrollMarketBusiness.Exceptions;
using TrollMarketBusiness.Interfaces;
using TrollMarketWeb.ViewModels.Cart;

namespace TrollMarketWeb.Services;

public class CartService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAccountRepository _accountRepository;

    public CartService(IOrderRepository orderRepository, IAccountRepository accountRepository)
    {
        _orderRepository = orderRepository;
        _accountRepository = accountRepository;
    }

    public CartIndexViewModel Get(string username)
    {
        var model = _accountRepository.Get(username);
        var cart = new List<CartViewModel>();

        foreach (var item in model.Orders)
        {
            if (item.OrderDate == null)
            {
                var orderViewModel = new CartViewModel()
                {
                    Id = item.Id,
                    Product = item.Product.Name,
                    Qty = item.Qty,
                    Shipment = item.Shipment.Name,
                    Seller = item.Product.Account.Name,
                    TotalPrice = ((item.Product.Price * item.Qty) + item.Shipment.Cost).ToString("C2", CultureInfo.CreateSpecificCulture("id-ID")),
                    OrderDate = item?.OrderDate
                };
                cart.Add(orderViewModel);
            }
        }

        return new CartIndexViewModel()
        {
            Carts = cart
        };
    }

    public void Update(CartIndexViewModel viewModel, string username)
    {
        var model = _orderRepository.Get(username);
        var buyer = _accountRepository.GetUsername(username);

        var totalPrice = model.Sum(item => (item.Product.Price * item.Qty) + item.Shipment.Cost);
        if (totalPrice > buyer.Balance)
        {
            throw new PurchaseAllException("Saldo anda kurang!");
        } 
        else {
            foreach (var item in model)
            {
                item.OrderDate = DateTime.Now;
                _orderRepository.Update(item);
                buyer.Balance = buyer.Balance - ((item.Product.Price * item.Qty) + item.Shipment.Cost);
                var seller = _accountRepository.GetSeller(item.Product.Account.Id);
                seller.Balance = seller.Balance + (item.Product.Price * item.Qty);
                _accountRepository.Update(buyer);
                _accountRepository.Update(seller);
            }
        }
    }

    public void Delete(int id)
    {
        var model = _orderRepository.GetById(id);
        _orderRepository.Delete(model);
    }
}
