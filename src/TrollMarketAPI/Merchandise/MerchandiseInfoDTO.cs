namespace TrollMarketAPI.Merchandise;

public class MerchandiseInfoDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string? Description { get; set; }
    public string Price { get; set; }
    public string? Discontinue { get; set; }
}
