﻿
using BakingAppDataLayer;
using BankingAppApiModels.Models;
using BankingAppApiModels.Models.Requests;
using DataAcces;

namespace BankingAppBusiness.TransactionRepo
{
    
    public interface ITransactionRepository
    {   
        Task<List<TransactionToApiModel>> GetTransactionReport(Guid transactionId,DateTimeOffset startDate , DateTimeOffset lastDate);
        Task CreateTransaction(CreateTransactionApiModel model);
        Task<List<TransactionToApiModel>> GetTransactions();
        Task<List<TransactionToApiModel>> GetAccountTransaction(Guid id);
    }
}
