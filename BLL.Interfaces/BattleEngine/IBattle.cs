using BLL.Interfaces.BattleEngine.Messages;
using BLL.Interfaces.BattleEngine.Units;
using BLL.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.BattleEngine
{
    public enum BattleStatus
    {
        Stopped,
        Killed,
        Active
    }

    public interface IBattle
    {
        string ID { get; }
        BattleStatus Status { get; }
        Result GetResult();
        Dictionary<string, BattleUnit> Units { get; }
        Dictionary<string, DetailedUserCharacteristics> PureCharacteristics { get; }
        void AddLog(ActionLog action);
    }
}
