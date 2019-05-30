using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RestaurantMng.WebApplication.SignalR
{
    public class RestaurantMngHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections =
        new ConnectionMapping<string>();

        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public void Send2(string message)
        {
            // Call the addNewMessageToPage method to update clients.
            //Clients.All.sendNoti( message);
        }

        public static void SendUser(string userId, object message)
        {
            var clients = GlobalHost.ConnectionManager.GetHubContext<RestaurantMngHub>().Clients;
            foreach (var connectionId in _connections.GetConnections(userId))
            {
                clients.Client(connectionId).sendNoti(message);
            }
        }

        public override Task OnConnected()
        {
            string name = "user1";

            _connections.Add(name, Context.ConnectionId);

            return base.OnConnected();
        }
        public override Task OnReconnected()
        {
            string name = "user1";

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}