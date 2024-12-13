namespace TrollMarketWeb.ViewModels.Shop;

public class ShopViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Price { get; set; }
    public bool? Discontinue { get; set; }
}
