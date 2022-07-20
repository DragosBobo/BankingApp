using BankingAppApiModels.Models.Account;
using BankingAppBusiness.AccountRepo;
using Microsoft.AspNetCore.Mvc;
namespace BankingAppControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _account;
        public AccountController(IAccountRepository account)
        {
            _account = account;
        }
        [HttpPost]
        public async Task<ActionResult> CreateAccount([FromBody] CreateAccountModel model)
        {
            await _account.AddAccount(model);
            return Ok("Account created with succes ! ");
        }

    }
}