using MicRq.Banking.Data.Context;
using MicRq.Banking.Domain.Interfaces;
using MicRq.Banking.Domain.Models;


namespace MicRq.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingDbContext _ctx;
        public AccountRepository(BankingDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Account> GetAccounts()
        {
             return _ctx.Accounts;
        }
    }
}
