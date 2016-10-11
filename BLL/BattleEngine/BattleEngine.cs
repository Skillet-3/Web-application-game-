using BLL.Interfaces.BattleEngine;
using BLL.Interfaces.BattleEngine.Executors;
using BLL.Interfaces.BattleEngine.Messages;
using BLL.Interfaces.BattleEngine.Validators;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.BattleEngine
{
    public class BattleEngine : IBattleEngine
    {
        private ConcurrentDictionary<string, IBattle> Battles = new ConcurrentDictionary<string, IBattle>();

        public void RegisterPreExecutor(IExecutor exec)
        {

        }

        public void RegisterPostExecutor(IExecutor exec)
        {

        }

        public void RegisterValidator(IValidator exec)
        {

        }

        public BattleResult Execute(IMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            IBattle battle = ValidateAndGet(message);
            if (battle != null)
            {
                PreExecution(message, battle);
                IExecutableMessage execMessage = message as IExecutableMessage;
                if (execMessage != null)
                {
                    execMessage.Execute(battle);
                }
                PostExecution(message, battle);
                var result = battle.GetResult();
                Dictionary<string, Reward> reward = null;
                if (battle.Status == BattleStatus.Killed)
                {
                    UnregisterBattle(battle.ID);
                    reward = GenerateReward(battle);
                }
                return new BattleResult() { Result = result, UserSpecificReward = reward, UsersStates = battle.Units };
            }
            throw new ArgumentException("message"); //change to a custom error

        }


        public void RegisterBattle(IBattle battle)
        {
            Battles.TryAdd(battle.ID, battle);
        }

        public void UnregisterBattle(string id)
        {
            IBattle battle;
            Battles.TryRemove(id, out battle);
        }

        private IBattle ValidateAndGet(IMessage message)
        {
            IBattle battle = null;
            Battles.TryGetValue(message.BattleId, out battle);
            return battle;
        }

        private void PreExecution(IMessage message, IBattle battle)
        {

        }

        private void PostExecution(IMessage message, IBattle battle)
        {

        }

        private Dictionary<string, Reward> GenerateReward(IBattle battle)
        {
            Dictionary<string, Reward> reward = new Dictionary<string, Reward>();
            foreach (var user in battle.Units)
            {
                if (!user.Value.Bot)
                    reward.Add(user.Key, new Reward() {
                        Experience = new Random().Next(1, 100),
                        Money = new Random().Next(1, 100),
                        Win = user.Value.Characteristics.Health > 0,
                        ID = user.Value.ID,
                        Name = user.Value.UserName
                    });
            }
            return reward;
        }
    }
}
