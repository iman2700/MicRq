using MicRq.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicRq.Domain.Commands
{
    public abstract class Command:Message
    {
        public DateTime TimeSpan { get;protected set; }
        protected Command() 
        {
            TimeSpan = DateTime.Now;
        }
    }
}
