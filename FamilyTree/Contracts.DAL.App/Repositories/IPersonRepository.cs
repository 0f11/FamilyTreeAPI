using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonRepository: IBaseRepository<Person>
    {
        Task<IEnumerable<Person>> AllAsync(Guid? userId = null);
        Task<Person> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        IEnumerable<Person> Include(params Expression<Func<Person, object>>[] includes);
    }
}