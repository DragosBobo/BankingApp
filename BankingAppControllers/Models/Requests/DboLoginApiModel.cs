using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppControllers.Models.Requests
{
    public class DboLoginApiModel
    {

         
        [Required]
        [EmailAddress]
        public string Email;

        [Required]
        public string Password;
    }
}
