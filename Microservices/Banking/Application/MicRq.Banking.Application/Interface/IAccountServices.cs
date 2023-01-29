using MicRq.Banking.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicRq.Banking.Application.Interface
{
    public interface IAccountServices
    {
        IEnumerable<Account> GetAccounts();
    }
}
