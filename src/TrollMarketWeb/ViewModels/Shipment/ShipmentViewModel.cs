namespace TrollMarketWeb.ViewModels.Shipment;

public class ShipmentViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Cost { get; set; }
    public string? IsService { get; set; }
}
