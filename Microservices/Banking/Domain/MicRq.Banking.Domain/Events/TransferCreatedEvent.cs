using MicRq.Banking.Domain.Commands;
using MicRq.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicRq.Banking.Domain.Events
{
    public class TransferCreatedEvent : MicRq.Domain.Event.Events
    { 
    
        public int From { get;private set; }  
        public int To { get;private set; }
        public decimal Amount { get;private set; }
        public TransferCreatedEvent(int from,int to,decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }
         
    }
}
