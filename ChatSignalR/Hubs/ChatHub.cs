using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace ChatSignalR.Hubs
{
    public class ChatHub : BaseHub
    {
        //static ConcurrentDictionary<string, string> userSessions = new ConcurrentDictionary<string, string>();
        public ChatHub()
        {
            InitMapping();
        }


        public override Task OnConnected()
        {
            var name = User.Identity.Name;
            if (UserMapping.GetConnections(name).Count() < 0)
            {
                Clients.AllExcept(Context.ConnectionId).userConnect(name);
            }
            Clients.Caller.connectedUsers(UserMapping.Content().Union(new[] { name }));
            return base.OnConnected();
        }

        public void Send(string message)
        {
            string pat = @"^[\s]*private[\s]*\[[a-zA-Z1-9]*]:[\s]*";
            Regex regex = new Regex(pat);

            if (regex.IsMatch(message))
            {
                string priv = regex.Match(message).Value;
                regex = new Regex(@"\[[a-zA-Z1-9]*]");
                string user = regex.Match(priv).Value.Trim('[', ']');
                Clients.Clients(UserMapping.GetConnections(user).ToList()).addNewMessageToPage(Context.User.Identity.Name, message);
                Clients.Caller.addNewMessageToPage(Context.User.Identity.Name, message);
            }
            else
            {
                Clients.All.addNewMessageToPage(Context.User.Identity.Name, message);
            }

        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var result =  base.OnDisconnected(stopCalled);
            Clients.AllExcept(Context.ConnectionId).userDisconnect(Context.ConnectionId);
            return result;
        }

        public void Hello()
        {
            Clients.All.hello();
        }
    }
}