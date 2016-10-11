using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        TEntity Get(string id);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> selector);
        IQueryable<TEntity> Get<TSortKey>(Expression<Func<TEntity, bool>> selector, Expression<Func<TEntity, TSortKey>> orderBy);
        IQueryable<TEntity> Get<TSortKey>(Expression<Func<TEntity, bool>> selector, Expression<Func<TEntity, TSortKey>> orderBy, int skip, int take);
        IQueryable<TEntity> Get<TSortKey>(Expression<Func<TEntity, bool>> selector, Expression<Func<TEntity, TSortKey>> orderBy, string firstElement, int take);
        void Update(TEntity entity);
        string Add(TEntity entity);
        void Delete(string id);
    }
}
