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
        [HttpPost("Register")]
        public async Task<ActionResult> AddUser(RegisterApiModel model)
        {
            var result = await _auth.Register(model);
            if (result)
            { return Ok("User created with succes"); }
            else
            {
                return BadRequest("User can't be created");
            }
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> LoginUser(LoginApiModel model)
        {
            var login = await _auth.Login(model);
            if( login == null )
            {
                return BadRequest($"Can't login {model.Email}");
             }
            else
            {
                return Ok(login);
            }

        }
    }

}