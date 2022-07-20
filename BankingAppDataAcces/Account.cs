using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakingAppDataLayer
{
    public class Account
    {
        public Guid Id { get; set; }

        public AccountType AccountType { get; set; }

        public Currency Currency { get; set; }

        public string Iban { get; set; }
        public Guid UserId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public virtual User User { get; set; }
    }
    public enum Currency { Ron, Euro, Dollar }
    public enum AccountType { Debit, Credit }
}
