using BankingAppApiModels.Models.Requests;
using BankingAppBusiness.AccountRepo;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

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
            await _accountRepository.AddAccount(model);

            return Ok("Account created with succes ! ");
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

            return Ok($"Account with id : {id} has been successfully updated !");
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteAccount(Guid id)
        {
            await _accountRepository.DeleteAccount(id);

            return Ok($"Account with id : {id} has been successfully deleted ! ");
        }
    }
}