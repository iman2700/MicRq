using MicRq.Banking.Application.Interface;
using MicRq.Banking.Application.Models;
using MicRq.Banking.Domain.Commands;
using MicRq.Banking.Domain.Interfaces;
using MicRq.Banking.Domain.Models;
using MicRq.Domain.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicRq.Banking.Application.Service
{
    public class AccountService : IAccountServices
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEventBus _eventBus;
        public AccountService(IAccountRepository accountRepository, IEventBus eventBus)
        {
            _accountRepository = accountRepository;
            _eventBus = eventBus;
        }
        
        public IEnumerable<Account> GetAccounts()
        {
            return _accountRepository.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            var createCommand = new CreateTransferCommand(
                 accountTransfer.FromAccount,
                 accountTransfer.ToAccount,
                 accountTransfer.TransferAmount
                );
            _eventBus.SendCommand(createCommand);
        }
    }
}
