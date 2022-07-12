using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class Account
    {
        public Guid Id { get; set; }

        public enum AccountType { Debit ,credit }

        public string IBan { get; set; }

        public enum Currency { Ron,Euro,Dollar }

        public Guid UserID { get; set; }


    }
}
