using BLL.Interfaces.BattleEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.BattleEngine.Units;

namespace BLL.BattleEngine.Battles
{
    public class BattleFactory : IBattleFactory
    {
        public IBattle Battle(BattleUnit unit, BattleUnit unit2)
        {
            return new BotBattle(unit, unit2) { ID = Guid.NewGuid().ToString()};
        }
    }
}
