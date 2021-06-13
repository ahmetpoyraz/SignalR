using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using SignalRProjectUI.Hubs;
using SignalRProjectUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TableDependency.SqlClient;

namespace SignalRProjectUI.Subsriptions.MiddleWare
{
    public interface IDatabaseSubscription
    {
        void Configure(string tableName);
    }
    public class DatabaseSubscription<T> : IDatabaseSubscription where T : class, new()
    {
        IConfiguration _configuration;
        IHubContext<ChatHub> _hubContext;

        public DatabaseSubscription(IConfiguration configuration , IHubContext<ChatHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        SqlTableDependency<T> _tableDependency;
        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("SQL"),tableName);
            _tableDependency.OnChanged += async (o, e) =>
            {
                Context context = new Context();

                 var result = context.ActiveUsers.ToList();
                
                
                await _hubContext.Clients.All.SendAsync("onlineUsers",result);
                
            };

            _tableDependency.OnError += (o, e) =>
            {
               
            };

           _tableDependency.Start();
        }

        ~DatabaseSubscription()
        {
            _tableDependency.Stop();
        }
    }
}
