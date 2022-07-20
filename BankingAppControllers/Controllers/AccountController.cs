using BankingAppApiModels.Models.Account;
using BankingAppBusiness.AccountRepo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await _account.createAccount(model);
            return Ok("Account created with succes ! ");
        }

    }
}