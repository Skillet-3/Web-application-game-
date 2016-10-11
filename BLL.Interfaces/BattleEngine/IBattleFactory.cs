using BLL.Interfaces.BattleEngine.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.BattleEngine
{
    public interface IBattleFactory
    {
        IBattle Battle(BattleUnit unit, BattleUnit unit2);
    }
}
