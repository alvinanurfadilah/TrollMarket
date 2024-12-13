using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using TrollMarketWeb.ViewModels.Admin;

namespace TrollMarketWeb.Services;

public class AdminService
{
    private readonly IAccountRepository _accountRepository;

    public AdminService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void Insert(AdminFormViewModel viewModel)
    {
        var model = new Account()
        {
            Username = viewModel.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            Role = viewModel.Role
        };

        _accountRepository.Insert(model);
    }
}
