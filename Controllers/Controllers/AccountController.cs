using BakingAppDataLayer;
using DataAcces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.Controllers
{
    [Route("[controller]")]
    [ApiController]
   public class AccountController:ControllerBase
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
       
        public async Task<ActionResult<ICollection<Account>>> GetAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
            
        }

        [HttpPost]

        public async Task<ActionResult<Account>> AddAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAccountsAsync), new {id=account.Id}, account);
        }
        
    }
}
