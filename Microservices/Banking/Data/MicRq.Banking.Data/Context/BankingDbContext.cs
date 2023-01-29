using Microsoft.EntityFrameworkCore;
using MicRq.Banking.Domain.Models;
 

namespace MicRq.Banking.Data.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
    }
}
