using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakingAppDataLayer
{
    public class User : IdentityUser
    {
       public Guid Id { get; set; }
       public virtual ICollection<Account> Accounts { get; set; }


    }
}
