namespace TrollMarketWeb.ViewModels.Merchandise;

public class MerchandiseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string? Discontinue { get; set; }
}
