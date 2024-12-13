namespace TrollMarketAPI.Carts;

public class DetailInfoDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string Description { get; set; }
    public string Price { get; set; }
    public string SellerName { get; set; }
}
