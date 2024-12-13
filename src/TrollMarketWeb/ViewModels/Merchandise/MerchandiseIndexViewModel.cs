using TrollMarketWeb.ViewModels;
using TrollMarketWeb.ViewModels.Merchandise;

namespace TrollMarketWeb;

public class MerchandiseIndexViewModel
{
    public List<MerchandiseViewModel> Merchandises { get; set; }
    public PaginationViewModel Pagination { get; set; }
}
