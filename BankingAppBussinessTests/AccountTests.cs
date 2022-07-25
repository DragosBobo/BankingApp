using BakingAppDataLayer;
using BankingAppApiModels.Models;
using BankingAppApiModels.Models.Requests;
using BankingAppBusiness.AccountRepo;
using DataAcces;
using Microsoft.EntityFrameworkCore;
using Currency = BankingAppApiModels.Models.Currency;
using AccountType = BankingAppApiModels.Models.AccountType;
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
            var account = new CreateAccountApiModel
            {
                AccountType = (BankingAppApiModels.Models.Requests.AccountType)1,
                Currency = (BankingAppApiModels.Models.Requests.Currency)(Currency)1,
                Iban = "RO0736183718293",
                UserId = new Guid(),
            };
            //Act 
            bool result = await sut.AddAccount(account);


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
                    AccountType = BakingAppDataLayer.AccountType.Credit,
                    Currency = BakingAppDataLayer.Currency.Ron,
                    Iban = "RO033373618371231238293",
                    UserId=new Guid("cff9d17f-bdfc-450d-a6c7-6aa8467383c8"),
                },
                new()
                {
                    AccountType = BakingAppDataLayer.AccountType.Debit,
                    Currency = BakingAppDataLayer.Currency.Euro,
                    Iban = "RO07361123138373318293",
                    UserId=new Guid("fc231452-7805-40a9-ae8c-9ac4743d4250"),
                }

            };
            context.Accounts.AddRange(accounts);
            await context.SaveChangesAsync();

            // Act 
            IEnumerable<AccountApiModel> result = await sut.GetAccounts();

            // Assert 
            Assert.AreEqual(accounts.Count, result.Count());
        }
        [TestMethod]
        public async Task TestGetAcountById()
        {
            //Arrange 
            var sut = new AccountRepository(context);
            var account = new Account
            {
                Id = new Guid("2df99e1f-ca5c-4c62-a444-c379b900cb96"),
                AccountType = BakingAppDataLayer.AccountType.Credit,
                Currency = BakingAppDataLayer.Currency.Ron,
                Iban = "RO033373618371231238293",
                UserId = new Guid("cff9d17f-bdfc-450d-a6c7-6aa8467383c8"),
            };
            var id = new Guid("2df99e1f-ca5c-4c62-a444-c379b900cb96");
            context.Accounts.Add(account);
            await context.SaveChangesAsync();

            //Act 
            Account result = await sut.GetAccountById(id);

            // Assert 
            Assert.IsNotNull(result);

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