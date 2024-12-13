using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrollMarketAPI.Carts;

public class CartFormDTO
{
    public int ProductId { get; set; }
    public int Qty { get; set; }
    public int AccountId { get; set; }
    public int ShipmentId { get; set; }

    public List<SelectListItem> Shipments { get; set; } = new List<SelectListItem>();
}
