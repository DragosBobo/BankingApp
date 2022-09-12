using BakingAppDataLayer;
using BankingAppBusiness.Auth;
using BankingAppControllers.Models.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BankingAppBussinessTests
{
    [TestClass]
    public class AuthRepositoryTests : BaseTest
    { 
        private readonly Mock<UserManager<User>> userManager;
        private readonly Mock<SignInManager<User>> signInManager;
        private readonly Mock<IUserStore<User>> store = new Mock<IUserStore<User>>();
        private readonly Mock<IHttpContextAccessor> contextAccesor = new Mock<IHttpContextAccessor>();
        private readonly Mock<IUserClaimsPrincipalFactory<User>> claimFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
        public AuthRepositoryTests()
        {
            userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<User, string>((x, y) => { context.Add(x); context.SaveChanges(); });
            signInManager = new Mock<SignInManager<User>>(userManager.Object, contextAccesor.Object, claimFactory.Object, null, null, null);
        }
        [TestMethod]

        public async Task TestRegisterShouldCreateUser()
        {   //Arange
            var authRepo = new AuthRepository(context, userManager.Object, signInManager.Object);
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
            await authRepo.Register(user);
            var userFound = context.Users.FirstOrDefault(x => x.FirstName == user.FirstName && x.LastName == user.LastName);

            //Assert
            context.Users.Should().HaveCount(1);
            userFound.Should().BeEquivalentTo(user, opts => opts.Excluding(si => si.Password).Excluding(si => si.ConfirmedPassword));
        }
        [TestMethod]

        public async Task TestLoginShouldLoginUser()
        {
            //Arange
            var authRepo = new AuthRepository(context, userManager.Object, signInManager.Object);
            var user = new RegisterApiModel
            {
                FirstName = "Popescu",
                LastName = "Marian",
                UserName = "PopescuMarian2000",
                Email = "Popescu.Marian@Gmail.com",
                Password = "Parola12345@",
                ConfirmedPassword = "Parola12345@"
            };
            var loginModel = new LoginApiModel
            {
                Email = user.Email,
                Password = user.Password,
            };

            //Act
            await authRepo.Register(user);
            var result = authRepo.Login(loginModel);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
