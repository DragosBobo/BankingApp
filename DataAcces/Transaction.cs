using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakingAppDataLayer
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }

        public  CategoryTransaction CategoryTransation{get;set;}

        public DateTimeOffset TransactionDate { get; set; }

        public Guid AccountId { get; set; }
        public virtual Account Account { get; set; }




    }
    public enum CategoryTransaction { }
}
