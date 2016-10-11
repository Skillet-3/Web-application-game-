namespace StandartORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Migrations;

    public partial class GameDB : DbContext
    {
        public GameDB()
            : base("name=GnomeDB")
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<GameDB>());
        }

        public virtual DbSet<ROLE> ROLES { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<SkillSet> SkillSets { get; set; }
        public virtual DbSet<Characteristics> Characteristics { get; set; }
        public virtual DbSet<State> States { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ROLE>()
                .HasMany(e => e.USERS)
                .WithMany(e => e.ROLES)
                .Map(m => m.ToTable("USER_ROLES"));

            modelBuilder.Entity<USER>()
                .HasRequired(e => e.SkillSet);

            modelBuilder.Entity<USER>()
                .HasRequired(e => e.Characteristics);

            modelBuilder.Entity<USER>()
                .HasRequired(e => e.State);
        }


    }
}
