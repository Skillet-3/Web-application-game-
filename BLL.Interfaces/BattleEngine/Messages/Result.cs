using BLL.Interfaces.BattleEngine.Units;
using BLL.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.BattleEngine.Messages
{
    public class ActionLog
    {
        public string ActionName { get; set; }
        public string AffectedUser { get; set; }
        public string SourceUser { get; set; }
        public Dictionary<string, int> Effects { get; set; }
    }

    public class Result
    {
        public string BattleId { get; set; }
        public BattleStatus Status { get; set; }
        public List<ActionLog> Log { get; set; }
    }

    public class BattleResult
    {
        public Dictionary<string, BattleUnit> UsersStates { get; set; }
        public Result Result { get; set; }
        public Dictionary<string, Reward> UserSpecificReward { get; set; }
    }

    public class Reward
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool Win { get; set; }
        public int Money { get; set; }
        public int Experience { get; set; }
    }
}
