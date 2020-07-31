using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IRelationshipsRepository: IBaseRepository<Relationships>
    {
        Task<IEnumerable<Relationships>> AllAsync(Guid? userId = null);
        Task<Relationships> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        IEnumerable<Relationships> Include(params Expression<Func<Relationships, object>>[] includes);
    }
}