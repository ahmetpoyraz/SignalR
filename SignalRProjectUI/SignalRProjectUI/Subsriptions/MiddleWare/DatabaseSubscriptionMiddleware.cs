using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRProjectUI.Subsriptions.MiddleWare
{
    static class DatabaseSubscriptionMiddleware
    {
        public static void UseDatabaseSubscription<T>(this IApplicationBuilder builder,string tableName ) where T : class, IDatabaseSubscription
        {
            var subsription = (T)builder.ApplicationServices.GetService(typeof(T));
            subsription.Configure(tableName);

        }
    }
}
