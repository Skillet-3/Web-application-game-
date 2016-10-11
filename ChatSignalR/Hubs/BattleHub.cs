using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using BLL.Interfaces.BattleEngine;
using BLL.Interfaces.BattleEngine.Units;
using BLL.Interfaces.BattleEngine.Messages;
using System.Timers;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using BLL.Interfaces;
using ChatSignalR.Hubs.Helpers;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ChatSignalR.Hubs
{
    public class BattleHub : BaseHub
    {
        IBattleEngine Engine;
        IBattleFactory Factory;
        JsonMessageBinder Binder;
        ICharacteristicService ChService;

        static ConcurrentDictionary<string, Timer> botTimers = new ConcurrentDictionary<string, Timer>();
        static ConcurrentQueue<BattleUnit> battleQueue = new ConcurrentQueue<BattleUnit>();

        public BattleHub(IBattleEngine engine, IBattleFactory factory, JsonMessageBinder binder, ICharacteristicService ChService)
        {
            Engine = engine;
            Factory = factory;
            Binder = binder;
            this.ChService = ChService;
            base.InitMapping();
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public string GetMessages()
        {
            var jo = new JObject();
            foreach (var pair in Binder.TypeIdDictionary)
            {
                jo.Add(pair.Key.FullName, pair.Value);
            }
            return jo.ToString();
        }

        public void InitBotBattle()
        {
            var userUnit = new BattleUnit()
            {
                UserName = Context.User.Identity.Name,
                ID = User.UserId,
                Bot = false,
                Characteristics = ChService.GetFullUserCharacteristics(User.UserId)
            };
            var bot = new BattleUnit()
            {
                UserName = "Bot",
                ID = Guid.NewGuid().ToString(),
                Bot = true,
                Characteristics = ChService.GetFullUserCharacteristics(User.UserId)
            };
            bot.Characteristics.Armor_head = 2;
            IBattle battle = Factory.Battle(userUnit, bot);

            Groups.Add(Context.ConnectionId, battle.ID).Wait();
            Engine.RegisterBattle(battle);
            Clients.Group(battle.ID).battleRegistered(battle.ID);

            // TODO: Modify batle! It should know who must be next.
            //Clients.Group(battle.ID).sendData(battle.ID); //Information about user. It should send who will start battle
        }

        public void InitBattle()
        {
            var userUnit = new BattleUnit()
            {
                UserName = Context.User.Identity.Name,
                ID = User.UserId,
                Bot = false,
                Characteristics = ChService.GetFullUserCharacteristics(User.UserId)
            };
            battleQueue.Enqueue(userUnit);
            if (battleQueue.Count >= 2)
            {
                Debug.WriteLine(battleQueue.Count);
                BattleUnit u1;
                BattleUnit u2;
                battleQueue.TryDequeue(out u1);
                battleQueue.TryDequeue(out u2);
                Debug.WriteLine(u1);
                Debug.WriteLine(u2);


                IBattle battle = Factory.Battle(u1, u2);
                foreach (var con in UserMapping.GetConnections(u1.UserName))
                    Groups.Add(con, battle.ID).Wait();
                foreach (var con in UserMapping.GetConnections(u2.UserName))
                    Groups.Add(con, battle.ID).Wait();
                Engine.RegisterBattle(battle);

                Clients.Group(battle.ID).sendData(
                    new
                    {
                        UserName = u1.UserName,
                        Hp = u1.Characteristics.Health,
                        Hp_Full = u1.Characteristics.Health_Full
                    },
                    new
                    {
                        UserName = u2.UserName,
                        Hp = u2.Characteristics.Health,
                        Hp_Full = u2.Characteristics.Health_Full
                    }
                    );
                Clients.Group(battle.ID).battleRegistered(battle.ID);
            }
        }

        private bool ExecuteMessage(IMessage message)
        {
            var result = Engine.Execute(message);
            if (result.Result.Status == BattleStatus.Killed)
            {
                foreach (var user in result.UserSpecificReward.Keys)
                {
                    var reward = result.UserSpecificReward[user];
                    ChService.SaveReward(reward, result.UsersStates[user].Characteristics);
                    Clients.Clients(UserMapping.GetConnections(user).ToList()).battleFinished(result.Result, reward);
                }
                return true;
            }
            else
            {
                Clients.Group(message.BattleId).battleResult(result.Result);
                return false;
            }
        }

        public void SendMessage(string msg)
        {
            IMessage message = Binder.Deserialize(msg, Context.User.Identity.Name);
            ExecuteMessage(message);
        }

        public void SendMessageBotBattle(string msg)
        {
            IMessage message = Binder.Deserialize(msg, Context.User.Identity.Name);
            if (!ExecuteMessage(message))
            {
                InitBot(message.BattleId);
            }
        }

        private void InitBot(string battleId)
        {
            var timer = new Timer(500);
            timer.Elapsed += (x, y) =>
            {
                timer.Stop();
                botTimers.TryRemove(battleId, out timer);
                var messageFactory = (IMessageFactory)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IMessageFactory));
                var message = messageFactory.GetMessage(battleId, "Bot");
                ExecuteMessage(message);
            };
            timer.Start();
            botTimers.TryAdd(battleId, timer);
        }

    }
}