using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProjectUI.Models
{
    public class ActiveUsers
    {
        public int Id { get; set; }
        public string connectionId { get; set; }
        public string userName { get; set; }
    }
}
