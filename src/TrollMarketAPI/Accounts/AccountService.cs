using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TrollMarketBusiness.Exceptions;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI.Accounts;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IConfiguration _configuration;

    public AccountService(IAccountRepository accountRepository, IConfiguration configuration)
    {
        _accountRepository = accountRepository;
        _configuration = configuration;
    }

    private string CreateToken(Account model)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, model.Username),
            new Claim(ClaimTypes.Role, model.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value
            )
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return serializedToken;
    }

    public string GetToken(AccountRequestDTO request)
    {
        var model = _accountRepository.Get(request.Username);
        bool isUsername = model.Username == request.Username;
        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(request.Password, model.Password);
        bool isCorrectRole = model.Role == request.Role;
        if (isUsername && isCorrectPassword && isCorrectRole)
        {
            return CreateToken(model);
        }

        throw new LoginException("Username or Password or Role is incorrect");
    }

    public void Update(AccountFormDTO dto)
    {
        var model = _accountRepository.Get(dto.Username);

        model.Balance = model.Balance + dto.Balance;

        _accountRepository.Update(model);
    }
}
