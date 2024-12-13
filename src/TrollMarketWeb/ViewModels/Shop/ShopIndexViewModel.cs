namespace TrollMarketWeb.ViewModels.Shop;

public class ShopIndexViewModel
{
    public List<ShopViewModel> Shops { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
}
