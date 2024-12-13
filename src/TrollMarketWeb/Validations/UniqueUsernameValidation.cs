using System.ComponentModel.DataAnnotations;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels.Account;
using TrollMarketWeb.ViewModels.Admin;

namespace TrollMarketWeb.Validations;

public class UniqueUsernameValidation : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var dbContext = (TrollMarketContext?)validationContext.GetService(typeof(TrollMarketContext)) ?? throw new NullReferenceException("System Error");

            var id = validationContext.ObjectInstance is AccountRegisterViewModel ? ((AccountRegisterViewModel)validationContext.ObjectInstance).Id : validationContext.ObjectInstance is AdminFormViewModel ? ((AdminFormViewModel)validationContext.ObjectInstance).Id : throw new InvalidOperationException("Invalid ViewModel type");
            
            var isExist = dbContext.Accounts.Any(
                acc => acc.Username == value.ToString() && acc.Id != id
            );

            if (isExist)
            {
                return new ValidationResult($"{value} already exist!");
            }
        }
        return ValidationResult.Success;
    } 
}
