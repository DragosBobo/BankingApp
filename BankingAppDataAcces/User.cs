using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakingAppDataLayer
{
    public class User : IdentityUser
    {
       public Guid Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
       public virtual ICollection<Account> Accounts { get; set; }
    }
}
