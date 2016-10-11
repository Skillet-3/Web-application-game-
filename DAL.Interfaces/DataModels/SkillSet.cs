using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DataModels
{
    public class SkillSet : IEntity
    {
        public string ID { get; set; }
        public int ThrustingWeapon { get; set; } // колющее
        public int BluntWeapon { get; set; } // дробящее
        public int SlashinggWeapon { get; set; } //режущее
        public int ChoppingWeapon { get; set; } //рубящее
        public int Magic { get; set; }
    }
}
