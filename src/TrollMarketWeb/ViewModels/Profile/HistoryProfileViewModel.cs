namespace TrollMarketWeb.ViewModels.Profile;

public class HistoryProfileViewModel
{
    public string Product { get; set; }
    public int Qty { get; set; }
    public string Shipment { get; set; }
    public string Seller { get; set; }
    public string TotalPrice { get; set; }
    public DateTime? OrderDate { get; set; }
}
