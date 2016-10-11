using AutoMapper;
using DAL.Interfaces;
using StandartORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realisation
{
    public class AbstractRepository<TEntity, DBEntity> : IRepository<TEntity>
        where TEntity: IEntity where DBEntity : IORMEntity
    {
        protected DbContext context;
        protected MapperConfiguration cfg;
        protected Expression<Func<DBEntity, TEntity>> expr;
        protected IMapper autoMapper;
        
        public AbstractRepository(DbContext context, MapperConfiguration cfg)
        {
            this.context = context;
            this.cfg = cfg;
            this.expr = cfg.ExpressionBuilder.CreateMapExpression<DBEntity, TEntity>();
            this.autoMapper = cfg.CreateMapper();
        }

        public virtual string Add(TEntity entity)
        {
            string id = Guid.NewGuid().ToString();
            DBEntity dbEntity = autoMapper.Map<DBEntity>(entity);//mapper.CastToORMEntity(entity);
            dbEntity.ID = id;
            context.Set<DBEntity>().Add(dbEntity);
            return id;
        }

        public virtual void Delete(string id)
        {
            var set = context.Set<DBEntity>();
            set.Remove(set.Where(x => x.ID.Equals(id)).First());
        }

        public virtual TEntity Get(string id)
        {
            var set = context.Set<DBEntity>();
            DBEntity dbEntity = set.First(x => x.ID.Equals(id));
            return autoMapper.Map<TEntity>(dbEntity);//mapper.CastToDALEntity(dbEntity);
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> selector)
        {
            var set = context.Set<DBEntity>();
            return set.Select(expr).Where(selector);
        }

        public virtual IQueryable<TEntity> Get<TSortKey>(Expression<Func<TEntity, bool>> selector, Expression<Func<TEntity, TSortKey>> orderBy)
        {
            return Get(selector).OrderBy(orderBy);
        }

        public virtual IQueryable<TEntity> Get<TSortKey>(Expression<Func<TEntity, bool>> selector, Expression<Func<TEntity, TSortKey>> orderBy, string firstElement, int take)
        {
            return Get(selector, orderBy).SkipWhile(x => x.ID.Equals(firstElement)).Take(take);
        }

        public virtual IQueryable<TEntity> Get<TSortKey>(Expression<Func<TEntity, bool>> selector, Expression<Func<TEntity, TSortKey>> orderBy, int skip, int take)
        {
            return Get(selector, orderBy).Skip(skip).Take(take);
        }

        public virtual void Update(TEntity entity)
        {
            string id = entity.ID;
            var set = context.Set<DBEntity>();
            var dbEntity = set.Where(x => x.ID.Equals(id)).First();
            autoMapper.Map(entity, dbEntity);
        }
        
    }
}
