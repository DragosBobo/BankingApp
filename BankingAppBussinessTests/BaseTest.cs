using DataAcces;
using Microsoft.EntityFrameworkCore;

namespace BankingAppBussinessTests
{
    public class BaseTest
    {
        protected readonly DataContext context;
        public BaseTest()
        {
            DbContextOptionsBuilder<DataContext> dbOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            context = new DataContext(dbOptions.Options);
        }

    }
}
