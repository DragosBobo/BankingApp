using BakingAppDataLayer;
using BankingAppBusiness.Auth;
using BankingAppControllers.Models.Requests;
using DataAcces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BankingAppBussinessTests
{  

    [TestClass]
    public class AuthRepositoryTests
    {
        private  readonly DataContext context;
        private readonly Mock<UserManager<User>> userManager;
        private readonly Mock<SignInManager<User>> signInManager;
        private readonly Mock<IUserStore<User>> store = new Mock<IUserStore<User>>();
        private readonly Mock<IHttpContextAccessor> contextAccesor = new Mock<IHttpContextAccessor>();
        private readonly Mock<IUserClaimsPrincipalFactory<User>> claimFactory = new Mock<IUserClaimsPrincipalFactory<User>>();

        public AuthRepositoryTests( )
        {
            DbContextOptionsBuilder<DataContext> dbOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new DataContext(dbOptions.Options);
            userManager = new Mock<UserManager<User>>(store.Object,null,null,null,null, null, null, null, null);
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<User, string>((x, y) => context.Add(x)); 
            signInManager = new Mock<SignInManager<User>>(userManager.Object,contextAccesor.Object,claimFactory.Object,null,null,null);
        }
        [TestMethod]
       
        public async Task TestRegister()
        {   //Arange
            var auth = new AuthRepository(context,userManager.Object,signInManager.Object);
            var sut = auth ;
            var user = new RegisterApiModel
            {
                FirstName = "Popescu",
                LastName = "Marian",
                UserName = "PopescuMarian2000",
                Email = "Popescu.Marian@Gmail.com",
                Password = "Parola12345@",
                ConfirmedPassword = "Parola12345@"
            };

            //Act
            var result = await sut.Register(user);
            var exists = await context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
           
            //Assert
            context.Users.Should().HaveCount(1);
            exists.Should().NotBeNull();
            Assert.IsTrue(result);

        }
       
    }
}
