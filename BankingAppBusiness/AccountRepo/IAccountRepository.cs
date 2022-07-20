using BakingAppDataLayer;
using BankingAppApiModels.Models.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppBusiness.AccountRepo
{
    public interface IAccountRepository
    {
        Task getAccounts();
        Task getAccountById();
        Task AddAccount(CreateAccountModel account);
        Task updateAccount();
        Task deleteAccount();
    }
}
