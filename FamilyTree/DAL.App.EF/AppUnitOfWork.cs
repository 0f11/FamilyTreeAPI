using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<ApplicationDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(ApplicationDbContext uowdContext) : base(uowdContext)
        {
        }

        public IContactRepository Contacts =>
            GetRepository<IContactRepository>(() => new ContactRepository(UOWDContext));

        public IContactTypeRepository ContactTypes =>
            GetRepository<IContactTypeRepository>(() => new ContactTypeRepository(UOWDContext));

        public IFamilyRepository Families => GetRepository<IFamilyRepository>(() => new FamilyRepository(UOWDContext));
        public IGenderRepository Genders => GetRepository<IGenderRepository>(() => new GenderRepository(UOWDContext));
        public IPersonRepository Persons => GetRepository<IPersonRepository>(() => new PersonRepository(UOWDContext));

        public IRelationshipsRepository Relationships =>
            GetRepository<IRelationshipsRepository>(() => new RelationshipRepository(UOWDContext));

        public IRelationshipRolesRepository RelationshipRoles =>
            GetRepository<IRelationshipRolesRepository>(() => new RelationshipRolesRepository(UOWDContext));

        public IRelationshipTypesRepository RelationshipTypes =>
            GetRepository<IRelationshipTypesRepository>(() => new RelationshipTypesRepository(UOWDContext));
    }
}