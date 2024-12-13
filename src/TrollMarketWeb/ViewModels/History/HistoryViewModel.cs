using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrollMarketWeb.ViewModels.History;

public class HistoryViewModel
{
    public string OrderDate { get; set; }
    public string Seller { get; set; }
    public string Buyer { get; set; }
    public string Product { get; set; }
    public int Qty { get; set; }
    public string Shipment { get; set; }
    public string TotalPrice { get; set; }
}