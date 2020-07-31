using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IContactRepository Contacts { get; }
        IContactTypeRepository ContactTypes { get; }
        IFamilyRepository Families { get; }
        IGenderRepository Genders { get; }
        IPersonRepository Persons { get; }
        IRelationshipsRepository Relationships { get; }
        IRelationshipRolesRepository RelationshipRoles { get; }
        IRelationshipTypesRepository RelationshipTypes { get; }
    }
}