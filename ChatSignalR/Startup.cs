using Owin;
using Microsoft.Owin;
using Ninject;
using Microsoft.AspNet.SignalR;
using ChatSignalR.Hubs;
using BLL.Interfaces.BattleEngine;
using BLL.Interfaces.BattleEngine.Messages;
using BLL.Interfaces;

[assembly: OwinStartup(typeof(SignalRChat.Startup))]
namespace SignalRChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here

            var resolver = System.Web.Mvc.DependencyResolver.Current;



            GlobalHost.DependencyResolver.Register(
                typeof(BattleHub),
                () => new BattleHub((IBattleEngine)resolver.GetService(typeof(IBattleEngine)),
                    (IBattleFactory)resolver.GetService(typeof(IBattleFactory)),
                    (JsonMessageBinder)resolver.GetService(typeof(JsonMessageBinder)),
                    (ICharacteristicService)resolver.GetService(typeof(ICharacteristicService))
                ));

            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            app.MapSignalR(hubConfiguration);
        }
    }
}
