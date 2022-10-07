
using BakingAppDataLayer;
using BankingAppApiModels.Models.Requests;
using BankingAppBusiness.TransactionRepo;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionController(ITransactionRepository transaction)
        {
            _transactionRepository = transaction;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTransaction(CreateTransactionApiModel model)
        {
            await _transactionRepository.CreateTransaction(model);
            return Ok("Transaction created with success !");
        }
        [HttpGet]
        public async Task<ActionResult> GetTransactions()
        {
            var result = await _transactionRepository.GetTransactions();

            return (result == null) ? NotFound() : Ok(result);
        }
        [HttpGet("/api/Transaction/raport")]
        public async Task<ActionResult> GetTransactioReport(Guid id,DateTimeOffset startDate,DateTimeOffset lastDate)
        {
            var result = await _transactionRepository.GetTransactionReport(id,startDate,lastDate);
            return Ok(result);
        }
        [HttpGet("/api/Transaction/id")]
        public async Task<ActionResult> GetAccountTransaction(Guid id)
        {
            var result = await _transactionRepository.GetAccountTransaction(id);
            return Ok(result);
        }
    }
}
