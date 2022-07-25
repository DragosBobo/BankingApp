using BakingAppDataLayer;

using DataAcces;
using Microsoft.EntityFrameworkCore;

using BankingAppBusiness.Auth;

using BankingAppControllers.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BankingAppBussinessTests
{
    [TestClass]
    public class AuthRepositoryTests
    {

        private readonly DataContext context;
        private readonly IAuthRepository authRepository;
        public AuthRepositoryTests()
        {

            DbContextOptionsBuilder<DataContext> dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(
                Guid.NewGuid().ToString());
            context = new DataContext(dbOptions.Options);
            
        }

        [TestMethod]
        public async Task TestRegister()
        {
            var test = authRepository;
           
            var user = new RegisterApiModel
            {
                FirstName = "Popescu",
                LastName = "Marian",
                UserName = "PopescuMarian2000",
                Email = "Popescu.Marian@Gmail.com",
                Password = "Parola12345@",
                ConfirmedPassword = "Parola12345@"
            };

            // Act
            bool result = await test.Register(user);

            //Assert 

            Assert.IsTrue(true);
        }
    }
}
