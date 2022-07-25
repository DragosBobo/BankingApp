using BakingAppDataLayer;
using BankingAppBusiness.Auth;
using BankingAppControllers.Models.Requests;
using DataAcces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var result = await _auth.Register(model);
            if (result)
            { return Ok("User created with succes"); }
            else return BadRequest();
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(LoginApiModel model)
        {
            var login = await _auth.Login(model);
            if( login == null )
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