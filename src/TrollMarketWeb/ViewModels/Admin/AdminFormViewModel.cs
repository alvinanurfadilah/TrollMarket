using System.ComponentModel.DataAnnotations;
using TrollMarketWeb.Validations;

namespace TrollMarketWeb.ViewModels.Admin;

public class AdminFormViewModel
{
    public int Id { get; set; }
    [Required]
    [StringLength(maximumLength:30)]
    [UniqueUsernameValidation]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    [Compare("Password", ErrorMessage = "Password are not the same")]
    public string ConfirmPassword { get; set; } = null!;
    public string Role { get; set; } = null!;
}
