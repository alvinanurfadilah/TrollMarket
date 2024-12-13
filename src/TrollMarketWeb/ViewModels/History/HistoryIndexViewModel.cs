using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrollMarketWeb.ViewModels.History;

public class HistoryIndexViewModel
{
    public List<HistoryViewModel> Histories { get; set; }
    public PaginationViewModel Pagination { get; set; }
    public int Buyer { get; set; }
    public int Seller { get; set; }

    public List<SelectListItem> Buyers { get; set; } = new List<SelectListItem>();
    public List<SelectListItem> Sellers { get; set; } = new List<SelectListItem>();
}
