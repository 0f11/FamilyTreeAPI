using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class RelationshipTypesRepository: EFBaseRepository<RelationshipTypes,ApplicationDbContext>,IRelationshipTypesRepository
    {
        public RelationshipTypesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<RelationshipTypes>> AllAsync(Guid? userId = null)
        {
            return await RepoDbSet.ToListAsync();
        }

        public async Task<RelationshipTypes> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
    }
}