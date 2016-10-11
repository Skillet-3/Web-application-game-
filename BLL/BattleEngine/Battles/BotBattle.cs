using BLL.Interfaces.BattleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.BattleEngine.Messages;
using BLL.Interfaces.BattleEngine.Units;
using BLL.Interfaces.Models;

namespace BLL.BattleEngine.Battles
{
    class BotBattle : IBattle
    {
        public string ID { get; set; }

        public BattleStatus Status { get; private set; }

        public Dictionary<string, BattleUnit> Units { get; private set; }

        public Dictionary<string, DetailedUserCharacteristics> PureCharacteristics { get; private set; }

        public Result StepResult;

        public BotBattle(BattleUnit unit, BattleUnit bot)
        {
            Units = new Dictionary<string, BattleUnit>();
            Units.Add(unit.UserName, unit);
            Units.Add(bot.UserName, bot);

            PureCharacteristics = new Dictionary<string, DetailedUserCharacteristics>();
            PureCharacteristics.Add(unit.UserName, unit.Characteristics);
            PureCharacteristics.Add(bot.UserName, bot.Characteristics);

            Status = BattleStatus.Active;
            StepResult = new Result() { Log = new List<ActionLog>()};
        }

        public void AddLog(ActionLog action)
        {
            StepResult.Log.Add(action);
        }

        public Result GetResult()
        {

            foreach(var unit in Units.Values)
            {
                if(unit.Characteristics.Health <= 0)
                {
                    Status = BattleStatus.Killed;
                }
            }
            StepResult.Status = Status;
            var temp = StepResult;
            temp.BattleId = ID;
            StepResult = new Result() { Log = new List<ActionLog>()};
            return temp;
        }
    }
}
