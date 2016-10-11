using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace StandartORM
{
    public class SkillSet : IORMEntity
    {
        [StringLength(50)]
        public override string ID { get; set; }

        [DefaultValue(0)]
        public int ThrustingWeapon { get;set; } // колющее
        [DefaultValue(0)]
        public int BluntWeapon { get; set; } // дробящее
        [DefaultValue(0)]
        public int SlashinggWeapon { get; set; } //режущее
        [DefaultValue(0)]
        public int ChoppingWeapon { get; set; } //рубящее
        [DefaultValue(0)]
        public int Magic { get; set; }

        public virtual USER User { get; set; }

    }
}
