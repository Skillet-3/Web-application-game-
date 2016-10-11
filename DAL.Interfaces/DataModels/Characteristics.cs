using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.DataModels
{
    public class Characteristics : IEntity
    {
        public string ID { get; set; }
        public int Power { get; set; }
        public int Agility { get; set; }

        public int Intuition { get; set; }

        public int Stamina { get; set; }

        public int Intelligence { get; set; }

        private int experience;

        public int Experience
        {
            get
            {
                return experience;
            }
            set
            {
                experience = value;
                Level = ResolveLevel();
            }
        }
        public int Level { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Standoff { get; set; }

        public string Clan { get; set; }
        public int Money { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }

        private int ResolveLevel()
        {
            return 1 + (int)Math.Sqrt(experience / 100);
        }
    }
}
