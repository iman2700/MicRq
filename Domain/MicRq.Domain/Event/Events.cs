using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicRq.Domain.Event
{
    public abstract class Events
    {
        public DateTime TimeSpan  { get; protected set; }
        protected Events() 
        {
            TimeSpan = DateTime.Now;
        }
    }
}
