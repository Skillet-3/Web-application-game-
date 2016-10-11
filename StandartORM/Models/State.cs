using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandartORM
{
    public class State : IORMEntity
    {
        [StringLength(50)]
        public override string ID { get; set; }

        public string Room { get; set; }

        [DefaultValue(0)]
        public int BattleState { get; set; }

        [DefaultValue(0)]
        public int CurrentHP { get; set; }
        [DefaultValue(0)]
        public int CurrentMP { get; set; }

        public virtual USER User { get; set; }
    }
}
