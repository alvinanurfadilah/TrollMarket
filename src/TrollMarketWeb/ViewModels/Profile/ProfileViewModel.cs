using TrollMarketWeb.ViewModels.Cart;

namespace TrollMarketWeb.ViewModels.Profile;

public class ProfileViewModel
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string? Name { get; set; }
    public string Role { get; set; } = null!;
    public string? Address { get; set; }
    public string? Balance { get; set; }
    public List<CartViewModel>? Orders { get; set; }
}
