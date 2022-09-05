using BakingAppDataLayer;
using BankingAppApiModels.Models;
using BankingAppApiModels.Models.Requests;
using BankingAppBusiness.AccountRepo;
using DataAcces;
using Microsoft.EntityFrameworkCore;
using Currency = BankingAppApiModels.Models.Currency;
using AccountType = BankingAppApiModels.Models.AccountType;
using Newtonsoft.Json;

namespace BankingAppBussinessTests
{
    [TestClass]
    public class AccountRepositoryTests
    {
        private readonly DataContext context;

        public AccountRepositoryTests()
        {
            DbContextOptionsBuilder<DataContext> dbOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new DataContext(dbOptions.Options);
        }

        [TestMethod]
        public async Task TestAddAccount()
        {
            //Arrange 
            var sut = new AccountRepository(context);
            var guid = new Guid();
            bool result;
            var account = new CreateAccountApiModel
            {
                AccountType = (BankingAppApiModels.Models.Requests.AccountType)1,
                Currency = (BankingAppApiModels.Models.Requests.Currency)(Currency)1,
                Iban = "RO0736183718293",
                UserId = guid,
            };
            //Act 
            await sut.AddAccount(account);
            var exists = await context.Accounts.FirstOrDefaultAsync(x => x.Id == guid);
            if(exists == null)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);

        }
        [TestMethod]
        public async Task TestGetAccounts()
        {
            //Arrange 
            var sut = new AccountRepository(context);
            var accounts = new List<Account>()
            {
                new()
                {
                    AccountType = BakingAppDataLayer.AccountType.Debit,
                    Currency = BakingAppDataLayer.Currency.Ron,
                    Iban = "RO033373618371231238293",
                    UserId=new Guid("cff9d17f-bdfc-450d-a6c7-6aa8467383c8"),
                },
                new()
                {
                    AccountType = BakingAppDataLayer.AccountType.Debit,
                    Currency = BakingAppDataLayer.Currency.Ron,
                    Iban = "RO07361123138373318293",
                    UserId=new Guid("fc231452-7805-40a9-ae8c-9ac4743d4250"),
                }
                
            };
            var expectedResultApi = new List<AccountApiModel>()
            {
                new() {
                    AccountType = 0,
                    Currency = 0,
                    Iban = "RO033373618371231238293",
                },
                new()
                {
                    AccountType = 0,
                    Currency = 0,
                    Iban = "RO07361123138373318293",
                }
            };
            context.Accounts.AddRange(accounts);
            await context.SaveChangesAsync();

            // Act 
            var response = await sut.GetAccounts();
            var result = JsonConvert.SerializeObject(response);
            var expectedResult = JsonConvert.SerializeObject(expectedResultApi);

            // Assert 
            Assert.AreEqual(expectedResultApi.Count, response.Count);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public async Task TestGetAcountById()
        {
            //Arrange 
            var sut = new AccountRepository(context);
            var id = new Guid("2df99e1f-ca5c-4c62-a444-c379b900cb96");
            var account = new Account
            {
                Id = new Guid("2df99e1f-ca5c-4c62-a444-c379b900cb96"),
                AccountType = BakingAppDataLayer.AccountType.Credit,
                Currency = BakingAppDataLayer.Currency.Ron,
                Iban = "RO033373618371231238293",
                UserId = id,
            };
            context.Accounts.Add(account);
            await context.SaveChangesAsync();

            //Act 
            Account result = await sut.GetAccountById(id);

            // Assert 
            Assert.AreEqual(JsonConvert.SerializeObject(account), JsonConvert.SerializeObject(result));

        }
        [TestMethod]
        public async Task TestUpdateAccount()
        {
            //Arrange
            var sut = new AccountRepository(context);
            var account = new Account
            {
                Id = new Guid("2df99e1f-ca5c-4c62-a444-c379b900cb86"),
                AccountType = BakingAppDataLayer.AccountType.Credit,
                Currency = BakingAppDataLayer.Currency.Ron,
                Iban = "RO033373618371231238293",
                UserId = new Guid("cff9d17f-bdfc-450d-a6c7-6aa8467383c8"),
            };
            context.Accounts.Add(account);
            await context.SaveChangesAsync();
            var model = new CreateAccountApiModel
            {
                AccountType = BankingAppApiModels.Models.Requests.AccountType.Credit,
                Currency = BankingAppApiModels.Models.Requests.Currency.Ron,
                UserId = new Guid("3386e917-4437-4342-94c1-f2693117d638"),
                Iban = "RO033373618371231238293",
            };
            var id = "2df99e1f-ca5c-4c62-a444-c379b900cb86";

            //Act 
            var result = await sut.UpdateAccount(new Guid(id), model);

            // Assert
            Assert.AreEqual(id, result);
        }
        [TestMethod]
        public async Task TestDeleteAccount()
        {
            //Arrange
            var sut = new AccountRepository(context);
            var account = new Account
            {
                Id = new Guid("2df99e1f-ca5c-4c62-a444-c379b900cb86"),
                AccountType = BakingAppDataLayer.AccountType.Credit,
                Currency = BakingAppDataLayer.Currency.Ron,
                Iban = "RO033373618371231238293",
                UserId = new Guid("cff9d17f-bdfc-450d-a6c7-6aa8467383c8"),
            };
            context.Accounts.Add(account);
            await context.SaveChangesAsync();
            var id = "2df99e1f-ca5c-4c62-a444-c379b900cb86";

            // Act
            var result = await sut.DeleteAccount(new Guid(id));

            //Assert
            Assert.AreEqual(id, result);
        }

    }
  
}