using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI.Shipments;

public class ShipmentService
{
    private readonly IShipmentRepository _repository;

    public ShipmentService(IShipmentRepository repository)
    {
        _repository = repository;
    }

    public ShipmentFormDTO Get(int id)
    {
        var model = _repository.GetById(id);

        return new ShipmentFormDTO()
        {
            Id = model.Id,
            Name = model.Name,
            Cost = model.Cost,
            IsService = model.IsService
        };
    }

    public void Insert(ShipmentFormDTO dto)
    {
        var model = new Shipment()
        {
            Id = dto.Id,
            Name = dto.Name,
            Cost = dto.Cost,
            IsService = dto.IsService
        };

        _repository.Insert(model);
    }

    public void Update(ShipmentFormDTO dto)
    {
        var model = _repository.GetById(dto.Id);
        model.Name = dto.Name;
        model.Cost = dto.Cost;
        model.IsService = dto.IsService;

        _repository.Update(model);
    }
}
