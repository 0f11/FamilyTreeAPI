using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository: EFBaseRepository<Person,ApplicationDbContext>,IPersonRepository
    {
        public PersonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Person>> AllAsync(Guid? userId = null)
        {
            return await RepoDbSet.ToListAsync();
        }

        public async Task<Person> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var owner = await FirstOrDefaultAsync(id, userId);
            base.Remove(owner);
        }
        public IEnumerable<Person> Include(params Expression<Func<Person, object>>[] includes)
        {
            IIncludableQueryable<Person, object> query = null;

            if(includes.Length > 0)
            {
                query = RepoDbSet.Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query.Include(includes[queryIndex]);
            }

            return query == null ? RepoDbSet : (IEnumerable<Person>)query;
        }
    }
}