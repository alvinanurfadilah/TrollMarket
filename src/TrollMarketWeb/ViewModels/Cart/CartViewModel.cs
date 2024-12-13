namespace TrollMarketWeb.ViewModels.Cart;

public class CartViewModel
{
    public string Username { get; set; }
    public int Id { get; set; }
    public string Product { get; set; }
    public int Qty { get; set; }
    public string Shipment { get; set; }
    public string Seller { get; set; }
    public string TotalPrice { get; set; }
    public DateTime? OrderDate { get; set; }
}
