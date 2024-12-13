using System.ComponentModel.DataAnnotations;
using TrollMarketWeb.Validations;

namespace TrollMarketWeb.ViewModels.Account;

public class AccountRegisterViewModel
{
    public int Id { get; set; }
    [Required]
    [UniqueUsernameValidation]
    [StringLength(maximumLength:30)]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Compare("Password", ErrorMessage = "Password are not the same")]
    public string ConfirmPassword { get; set; } = null!;
    public string Role { get; set; } = null!;
    [Required]
    [StringLength(maximumLength:100)]
    public string? Name { get; set; }
    [Required]
    [StringLength(maximumLength:500)]
    public string? Address { get; set; }
}