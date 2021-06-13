using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProjectUI.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string Sender { get; set; }

        public string Receiver { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }

    }
    
}
