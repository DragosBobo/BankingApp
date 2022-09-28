using BankingAppApiModels.Models.Requests;
using BankingAppBusiness.AccountRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpPost]
        public async Task<ActionResult> CreateAccount([FromBody] CreateAccountApiModel model)
        {
            var response = await _accountRepository.AddAccount(model);

            return Ok(response);
        }
        [HttpGet]

        public async Task<ActionResult> GetAccounts()
        {
            var result = await _accountRepository.GetAccounts();


            return (result == null) ? NotFound() : Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAccountById(Guid id)
        {
            var result = await _accountRepository.GetAccountById(id);

            return (result == null) ? NotFound() : Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateAccount(Guid id, CreateAccountApiModel model)
        {
            await _accountRepository.UpdateAccount(id, model);

            return Ok(id);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteAccount(Guid id)
        {
            await _accountRepository.DeleteAccount(id);

            return Ok(id);
        }
    }
}