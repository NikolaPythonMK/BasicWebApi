using BasicWebApi.Core.Services;
using BasicWebApi.Domain.Entities;
using BasicWebApi.Persistance;

namespace BasicWebApi.Core.Repositories.Implementations
{
    public class GenericRepository<T> : Repository<T> where T : BaseEntity
    {
        public GenericRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
