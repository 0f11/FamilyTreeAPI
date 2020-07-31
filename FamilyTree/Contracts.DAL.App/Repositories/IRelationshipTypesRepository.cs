using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IRelationshipTypesRepository: IBaseRepository<RelationshipTypes>
    {
        Task<IEnumerable<RelationshipTypes>> AllAsync(Guid? userId = null);
        Task<RelationshipTypes> FirstOrDefaultAsync(Guid id, Guid? userId = null);

        Task<bool> ExistsAsync(Guid id, Guid? userId = null);
        Task DeleteAsync(Guid id, Guid? userId = null);
        
    }
}