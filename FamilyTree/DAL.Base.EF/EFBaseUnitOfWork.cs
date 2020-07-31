using System.Threading.Tasks;
using Contracts.DAL.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class EFBaseUnitOfWork<TDbContext> : BaseUnitOfWork, IBaseUnitOfWork
        where TDbContext : DbContext
    {
        protected TDbContext UOWDContext;

        public EFBaseUnitOfWork(TDbContext uowdContext)
        {
            UOWDContext = uowdContext;
        }

        public int SaveChanges()
        {
            return UOWDContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await UOWDContext.SaveChangesAsync();
        }
    }
}