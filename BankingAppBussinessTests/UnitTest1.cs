using BankingAppApiModels.Models.Requests;
using BankingAppBusiness.AccountRepo;
using DataAcces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankingAppBussinessTests
{
    [TestClass]
    public class AccountRepositoryShould
    {

        private readonly DataContext context;

        public AccountRepositoryShould()
        {
            DbContextOptionsBuilder dbOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(
                Guid.NewGuid().ToString()
                );
            context = new DataContext(dbOptions.Options);
        }

        [TestMethod]
        public async Task TestAddAccount()
        {
            // Arrange 
            var sut = new AccountRepository(context);
            var account = new CreateAccountApiModel
            {
                AccountType = (AccountType)1,
                Currency = (Currency)1,
                Iban = "RO0736183718293",
                UserId = new Guid(),
            };
            // Act 
            bool result = await sut.AddAccount(account);


            // Assert
            Assert.IsTrue(result);
            
        }
    }
}