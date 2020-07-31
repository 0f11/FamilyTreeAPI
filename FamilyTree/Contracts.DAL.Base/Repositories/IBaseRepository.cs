using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseRepository<TEntity,Guid>
        where TEntity:class, IDomainEntity<Guid>, new()
    {
        
    }
    public interface IBaseRepository<TEntity, TKey>
        where TEntity : class, IDomainEntity<TKey>, new()
        where TKey : struct, IComparable
    {
        // crud
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();
        
        //IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null);
        //Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null);

        TEntity Find(params object[] id);
        Task<TEntity> FindAsync(params object[] id);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
        TEntity Remove(params object[] id);
        int SaveChanges();
        Task<int> SaveChangesAsync();
        //public IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions);

    }

}