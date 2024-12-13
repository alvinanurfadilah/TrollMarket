using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketBusiness.Exceptions;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels;
using TrollMarketWeb.ViewModels.Account;

namespace TrollMarketWeb.Services;

public class AccountSerice
{
    private readonly IAccountRepository _repository;

    public AccountSerice(IAccountRepository repository)
    {
        _repository = repository;
    }

    private List<SelectListItem> GetRoles()
    {
        return new List<SelectListItem>()
        {
            new SelectListItem()
            {
                Text = "Admin",
                Value = "Admin"
            },
            new SelectListItem()
            {
                Text = "Seller",
                Value = "Seller"
            },
            new SelectListItem()
            {
                Text = "Buyer",
                Value = "Buyer"
            }
        };
    }

    private ClaimsPrincipal GetPrincipal(AccountLoginViewModel viewModel)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, viewModel.Username),
            new Claim(ClaimTypes.Role, viewModel.Role??string.Empty)
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }

    private AuthenticationTicket GetTicket(ClaimsPrincipal principal)
    {
        AuthenticationProperties authenticationProperties = new AuthenticationProperties()
        {
            IssuedUtc = DateTime.Now,
            ExpiresUtc = DateTime.Now.AddMinutes(30),
            AllowRefresh = false
        };

        AuthenticationTicket authenticationTicket = new AuthenticationTicket(principal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme);

        return authenticationTicket;
    }

    public AccountLoginViewModel GetLogin()
    {
        return new AccountLoginViewModel()
        {
            Roles = GetRoles()
        };
    }

    public AuthenticationTicket SetLogin(AccountLoginViewModel viewModel)
    {
        var model = _repository.Get(viewModel.Username);
        bool isUsername = model.Username == viewModel.Username;
        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(viewModel.Password, model.Password);
        bool isCorrectRole = model.Role == viewModel.Role;
        if (isUsername && isCorrectPassword && isCorrectRole)
        {
            viewModel = new AccountLoginViewModel()
            {
                Username = model.Username,
                Password = model.Password,
                Role = model.Role
            };

            ClaimsPrincipal principal = GetPrincipal(viewModel);
            AuthenticationTicket ticket = GetTicket(principal);

            return ticket;
        }
        throw new LoginException("Username or Password or Role is invalid!");
    }

    public void Register(AccountRegisterViewModel viewModel)
    {
        var model = new Account()
        {
            Username = viewModel.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            Role = viewModel.Role,
            Name = viewModel.Name,
            Address = viewModel.Address
        };

        _repository.Insert(model);
    }
}
