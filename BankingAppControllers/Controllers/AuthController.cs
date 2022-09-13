using BankingAppBusiness.Auth;
using BankingAppControllers.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingAppControllers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _auth;
        public AuthController(IAuthRepository auth)
        {
            _auth = auth;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> AddUser([FromBody] RegisterApiModel model)
        {
            await _auth.Register(model);
            return Ok("Succes Register");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(LoginApiModel model)
        {
            var login = await _auth.Login(model);
            if (login == null)
            {
                return BadRequest("Can't login ");
            }
            else
            {
                return Ok(login);
            }
        }
    }
}