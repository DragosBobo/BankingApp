
using BakingAppDataLayer;
using BankingAppApiModels.Models;
using BankingAppApiModels.Models.Requests;
using DataAcces;

namespace BankingAppBusiness.TransactionRepo
{
    
    public interface ITransactionRepository
    {   
        Task<List<TransactionToApiModel>> GetTransactioReport(Guid transactionId,DateTimeOffset startDate , DateTimeOffset lastDate);
        Task<bool> CreateTransaction(CreateTransactionApiModel model);
        Task<Transaction> GetTransactionById(Guid transactionId);
        Task<List<Transaction>> GetTransactions();
    }
}
