using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatSignalR.Models
{
    public class InventoryViewModel
    {
        public string Name { get; set; }

        public int Power { get; set; }
        public int Agility { get; set; }
        public int Intuition { get; set; }
        public int Stamina { get; set; }
        public int Intelligence { get; set; }

        public int Experience { get; set; }
        public int Level { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Standoff { get; set; }

        public string Clan { get; set; }
        public int Money { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }

        public int Health_Full { get; set; }
        public int Mana_Full { get; set; }

        public int Damage_min { get; set; }
        public int Damage_max { get; set; }

        public int Crit { get; set; }
        public int Anti_crit { get; set; }
        public int Dodge { get; set; }
        public int Anti_dodge { get; set; }
        public int Armor_head { get; set; }
        public int Armor_body { get; set; }
        public int Armor_belt { get; set; }
        public int Armor_feet { get; set; }

    }
}