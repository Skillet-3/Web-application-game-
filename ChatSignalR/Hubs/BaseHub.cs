using ChatSignalR.Hubs.Helpers;
using ChatSignalR.Modules.Authentication;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChatSignalR.Hubs
{
    public abstract class BaseHub : Hub
    {
        static ConcurrentDictionary<Type, ConnectionMapping<string>> UserMappings = new ConcurrentDictionary<Type, ConnectionMapping<string>>();

        public void InitMapping()
        {
            var type = GetType();
            UserMappings.TryAdd(type, new ConnectionMapping<string>());
        }

        public ConnectionMapping<string> UserMapping
        {
            get
            {
                ConnectionMapping<string> mapping;
                UserMappings.TryGetValue(this.GetType(), out mapping);
                return mapping;
            }
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;

            UserMapping.Add(name, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;

            UserMapping.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;

            if (!UserMapping.GetConnections(name).Contains(Context.ConnectionId))
            {
                UserMapping.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }

        public CustomPrincipal User
        {
            get
            {
                return base.Context.User as CustomPrincipal;
            }
        }
    }
}