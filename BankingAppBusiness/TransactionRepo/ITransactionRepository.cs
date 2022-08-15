
using BakingAppDataLayer;
using BankingAppApiModels.Models;
using BankingAppApiModels.Models.Requests;
using DataAcces;

namespace BankingAppBusiness.TransactionRepo
{
    
    public interface ITransactionRepository
    {   
        Task<List<TransactionToApiModel>> GetTransactionReport(Guid transactionId,DateTimeOffset startDate , DateTimeOffset lastDate);
        Task<bool> CreateTransaction(CreateTransactionApiModel model);
        Task<TransactionToApiModel> GetTransactionById(Guid transactionId);
        Task<List<TransactionToApiModel>> GetTransactions();
        Task<List<TransactionToApiModel>> GetAccountTransaction(Guid id);
    }
}
