using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IRelationshipRolesRepository: IBaseRepository<RelationshipRoles>
    {
        Task<IEnumerable<RelationshipRoles>> AllAsync(Guid? userId = null);
        Task<RelationshipRoles> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
    }
}