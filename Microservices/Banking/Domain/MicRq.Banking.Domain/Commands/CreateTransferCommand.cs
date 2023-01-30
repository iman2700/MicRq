﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicRq.Banking.Domain.Commands
{
    public class CreateTransferCommand:TransferCommand
    {
        public CreateTransferCommand(int to ,int from,decimal amount) 
        {
            From = from;
            To = to;
            Amount = amount;   

        }

    }
}
