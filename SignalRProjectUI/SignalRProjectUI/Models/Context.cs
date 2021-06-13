using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProjectUI.Models
{
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-IE9IK3V3\\SQLEXPRESS; database=SignalRDb; integrated security=true;");
        }

        public DbSet<User> users { get; set; }
        public DbSet<ActiveUsers> ActiveUsers { get; set; }
        public DbSet<Messages> Messages { get; set; }
        
    }
}
