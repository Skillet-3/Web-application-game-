using DAL.DataModels;
using StandartORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;

namespace DAL.Realisation
{
    public class UserDetailedRepository : AbstractRepository<User, USER>
    {
        public UserDetailedRepository(DbContext context, MapperConfiguration cfg) : base(context, cfg)
        {
        }

        public override string Add(User entity)
        {
            string id = Guid.NewGuid().ToString();
            USER dbEntity = autoMapper.Map<USER>(entity);
            dbEntity.ID = id;
            dbEntity.ROLES = new List<ROLE>();
            GenerateRoles(entity, dbEntity);
            context.Set<USER>().Add(dbEntity);
            context.Set<SkillSet>().Add(new SkillSet() { ID = id });
            context.Set<Characteristics>().Add(new Characteristics() { ID = id, Power = 5, Agility = 5, Clan = "нет", Health = 100, Intelligence = 5, Intuition = 5, Level = 1, Mana = 100, Money = 10, Stamina = 5  });
            context.Set<State>().Add(new State() { ID = id , CurrentHP = 100, CurrentMP = 100});
            return id;
        }

        public override void Update(User entity)
        {
            string id = entity.ID;
            var set = context.Set<USER>();
            var dbEntity = set.Where(x => x.ID.Equals(id)).First();
            if (string.IsNullOrEmpty(entity.Password))
            {
                entity.Password = dbEntity.PASSWORD;
            }
            autoMapper.Map(entity, dbEntity);
            GenerateRoles(entity, dbEntity);
        }

        protected void GenerateRoles(User entity, USER dbEntity)
        {
            var roles = context.Set<ROLE>();

            foreach (var role in entity.Roles)
            {
                ROLE dbRole = roles.FirstOrDefault(x => x.ROLENAME.Equals(role));
                if (dbRole == null)
                {
                    dbRole = new ROLE() { ID = Guid.NewGuid().ToString(), ROLENAME = role };
                }
                dbEntity.ROLES.Add(dbRole);
            }
        }
    }
}
