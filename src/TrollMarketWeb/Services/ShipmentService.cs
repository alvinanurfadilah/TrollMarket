using System.Globalization;
using TrollMarketBusiness.Interfaces;
using TrollMarketWeb.ViewModels;
using TrollMarketWeb.ViewModels.Shipment;
using static TrollMarketWeb.ViewModels.Constant;

namespace TrollMarketWeb.Services;

public class ShipmentService
{
    private readonly IShipmentRepository _repository;

    public ShipmentService(IShipmentRepository repository)
    {
        _repository = repository;
    }

    public ShipmentIndexViewModel Get(int pageNumber)
    {
        var model = _repository.Get(pageNumber, PageSize)
        .Select(shipment => new ShipmentViewModel()  {
            Id = shipment.Id,
            Name = shipment.Name,
            Cost = shipment.Cost.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID")),
            IsService = shipment.IsService == true ? "Yes" : "No"
        });

        return new ShipmentIndexViewModel()
        {
            Shipments = model.ToList(),
            Pagination = new PaginationViewModel() {
                PageNumber = pageNumber,
                PageSize = PageSize,
                TotalRows = _repository.Count()
            }
        };
    }

    public void StopService(int id)
    {
        var model = _repository.GetById(id);
        model.IsService = false;
        _repository.Update(model);
    }

    public void Delete(int id)
    {
        var model = _repository.GetById(id);
        _repository.Delete(model);
    }
}
