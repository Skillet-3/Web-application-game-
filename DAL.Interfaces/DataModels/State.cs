using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DataModels
{
    public class State : IEntity
    {
        public string ID { get; set; }
        public string Room { get; set; }
        
        public int BattleState { get; set; }
        
        public int CurrentHP { get; set; }
        public int CurrentMP { get; set; }
    }
}
