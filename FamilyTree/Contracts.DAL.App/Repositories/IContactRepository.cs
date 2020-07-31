using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        Task<IEnumerable<Contact>> AllAsync(Guid? userId = null);
        Task<Contact> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        IEnumerable<Contact> Include(params Expression<Func<Contact, object>>[] includes);

    }
}