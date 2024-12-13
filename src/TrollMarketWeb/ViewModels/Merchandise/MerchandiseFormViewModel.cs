using System.ComponentModel.DataAnnotations;

namespace TrollMarketWeb.ViewModels.Merchandise;

public class MerchandiseFormViewModel
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string CategoryName { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    public int AccountId { get; set; }
    public bool Discontinue { get; set; }
}
