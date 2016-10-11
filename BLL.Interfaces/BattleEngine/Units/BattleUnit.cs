using BLL.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces.BattleEngine.Units
{
    public class BattleUnit
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public bool Bot { get; set; }
        public DetailedUserCharacteristics Characteristics { get; set; }
    }
}
