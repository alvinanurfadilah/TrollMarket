using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketBusiness.Interfaces;
using TrollMarketWeb.ViewModels;
using TrollMarketWeb.ViewModels.History;
using static TrollMarketWeb.ViewModels.Constant;

namespace TrollMarketWeb.Services;

public class HistoryService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAccountRepository _accountRepository;

    public HistoryService(IOrderRepository orderRepository, IAccountRepository accountRepository)
    {
        _orderRepository = orderRepository;
        _accountRepository = accountRepository;
    }

    private List<SelectListItem> GetBuyer()
    {
        var model = _accountRepository.Get()
        .Where(buyer => buyer.Role == "Buyer")
        .Select(buyer => new SelectListItem() {
            Text = buyer.Name,
            Value = buyer.Id.ToString()
        }).ToList();

        return model;
    }

    private List<SelectListItem> GetSeller()
    {
        var model = _accountRepository.Get()
        .Where(buyer => buyer.Role == "Seller")
        .Select(buyer => new SelectListItem() {
            Text = buyer.Name,
            Value = buyer.Id.ToString()
        }).ToList();

        return model;
    }

    public HistoryIndexViewModel Get(int pageNumber, int buyer, int seller)
    {
        var model = _orderRepository.Get(pageNumber, PageSize, buyer, seller)
        .Select(history => new HistoryViewModel()
        {
            OrderDate = history.OrderDate?.ToString("dd/MM/yyyy"),
            Seller = history.Product.Account.Name,
            Buyer = history.Account.Name,
            Product = history.Product.Name,
            Qty = history.Qty,
            Shipment = history.Shipment.Name,
            TotalPrice = ((history.Product.Price * history.Qty) + history.Shipment.Cost).ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"))
        });

        return new HistoryIndexViewModel()
        {
            Histories = model.ToList(),
            Pagination = new PaginationViewModel() {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _orderRepository.Count(buyer, seller)
            },
            Buyer = buyer,
            Seller = seller,
            Buyers = GetBuyer(),
            Sellers = GetSeller()
        };
    }
}
