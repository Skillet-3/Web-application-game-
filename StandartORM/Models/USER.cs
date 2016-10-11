namespace StandartORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USERS")]
    public partial class USER : IORMEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            ROLES = new HashSet<ROLE>();
            RegistrationDate = DateTime.UtcNow;
        }

        [StringLength(50)]
        public override string ID { get; set; }

        [Required]
        [StringLength(50)]
        public string USERNAME { get; set; }

        [Required]
        [StringLength(50)]
        public string PASSWORD { get; set; }

        [Required]
        [StringLength(50)]
        public string EMAIL { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public DateTime RegistrationDate { get; set; }

        public virtual SkillSet SkillSet { get; set; }
        public virtual Characteristics Characteristics { get; set; }
        public virtual State State { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<ROLE> ROLES { get; set; }
}
}
