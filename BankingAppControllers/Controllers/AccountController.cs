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
        public AccountController(IAccountRepository account)
        {
            _accountRepository = account;
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
            var result = await _accountRepository.getAccounts();

            return (result == null) ? NotFound() : Ok(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetAccountById(Guid id)
        {
            var result = await _accountRepository.getAccountById(id);

            return(result == null) ? NotFound() : Ok(result);   
        }
        [HttpPut]
        public async Task<ActionResult> UpdateAccount(Guid id, CreateAccountApiModel model)
        {
            await _accountRepository.updateAccount(id, model);

            return Ok($"Account with id : {id} has been successfully updated !");
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteAccount(Guid id)
        {
            await _accountRepository.deleteAccount(id);

            return Ok($"Account with id : {id} has been successfully deleted ! ");
        }
    }
}