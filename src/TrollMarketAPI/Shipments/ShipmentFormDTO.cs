namespace TrollMarketAPI.Shipments;

public class ShipmentFormDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Cost { get; set; }
    public bool? IsService { get; set; }
}
