
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using SignalRProjectUI.DTO;
using SignalRProjectUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace SignalRProjectUI.Hubs
{

    public class ChatHub : Hub
    {
        Context db = new Context();
        public override async Task OnConnectedAsync()
        {
            var userName = Context.User.Identity.Name;
            var connId = Context.ConnectionId;
            var result = db.ActiveUsers.FirstOrDefault(x => x.userName == userName);
            if (result == null)
            {
                ActiveUsers userList = new ActiveUsers()
                {
                    connectionId = connId,
                    userName = userName.Trim()
                };

                db.ActiveUsers.Add(userList);
                db.SaveChanges();
                await Clients.Others.SendAsync("userJoined", userName);
            }





        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userName = Context.User.Identity.Name;
            var connId = Context.ConnectionId;

            var delete = db.ActiveUsers.FirstOrDefault(x => x.userName == userName);
            if (delete != null)
            {
                db.ActiveUsers.Remove(delete);
                db.SaveChanges();

                await Clients.Others.SendAsync("userDisconnected", userName);
            }




        }
        public async Task SendMessageAsync(string message, string activeuser)
        {

            var userName = Context.User.Identity.Name;

            var user = db.ActiveUsers.FirstOrDefault(x => x.userName == activeuser);

            if (user!=null)
            {
                var userConn = user.connectionId;
                var date = DateTime.Now.ToString("MM/dd/yyyy");
                var hour = DateTime.Now.ToString("HH:mm");
                Messages messages = new Messages()
                {
                    Message = message,
                    Sender = userName,
                    Receiver = activeuser,
                    Date = date,
                    Hour = hour
                   
                };

                db.Messages.Add(messages);
                db.SaveChanges();


                await Clients.Client(userConn).SendAsync("receiveMessage", messages);

              

            }
           

        }

        public async Task OnlineUsers(string dataList)
        {

            await Clients.Others.SendAsync("onlineUsers", dataList);

        }



    }
}
