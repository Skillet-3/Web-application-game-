using BLL.Interfaces.BattleEngine.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.BattleEngine;
using Newtonsoft.Json;

namespace BLL.BattleEngine.Messages
{
    public class SimpleExecutableMessage : IExecutableMessage
    {
        public string BattleId { get; set; }

        public string MessageKey { get; set; }

        [JsonIgnore]
        public byte[] SrcData { get; set; }
        
        public string Unit { get; set; }

        public virtual void Execute(IBattle battle)
        {
            var enemy = battle.Units.Values.FirstOrDefault(x => x.UserName != Unit);
            var unit = battle.Units[Unit];
            int attack = -(new Random().Next(unit.Characteristics.Damage_min, unit.Characteristics.Damage_max));
            enemy.Characteristics.Health += attack;
            var effects = new Dictionary<string, int>();
            effects.Add("HP", attack);
            battle.AddLog(new ActionLog() { ActionName = "Simple atack", AffectedUser = enemy.UserName, Effects = effects, SourceUser = Unit });
        }
    }
}
