using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatSignalR.Models
{
    public class FightViewModel
    {
        public InventoryViewModel Person { get; set; }
        public InventoryViewModel PersonEnemy { get; set; }

    }
}