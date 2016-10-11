using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandartORM
{
    public class Characteristics : IORMEntity
    {
        [StringLength(50)]
        public override string ID { get; set; }

        [DefaultValue(0)]
        public int Power { get; set; }
        [DefaultValue(0)]
        public int Agility { get; set; }
        [DefaultValue(0)]
        public int Intuition { get; set; }
        [DefaultValue(0)]
        public int Stamina { get; set; }
        [DefaultValue(0)]
        public int Intelligence {get;set; }

        [DefaultValue(0)]
        public int Experience { get; set; }
        [DefaultValue(0)]
        public int Level { get; set; }
        [DefaultValue(0)]
        public int Wins { get; set; }
        [DefaultValue(0)]
        public int Losses { get; set; }
        [DefaultValue(0)]
        public int Standoff { get; set; }

        public string Clan { get; set; }
        [DefaultValue(0)]
        public int Money { get; set; }
        [DefaultValue(0)]
        public int Health { get; set; }
        [DefaultValue(0)]
        public int Mana { get; set; }


        public virtual USER User { get; set; }
    }
}
