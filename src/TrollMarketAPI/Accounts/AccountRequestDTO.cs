namespace TrollMarketAPI.Accounts;

public class AccountRequestDTO
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
}
