using BasicWebApi.Core.Repositories.Implementations;
using BasicWebApi.Core.Repositories.Interfaces;
using BasicWebApi.Core.Services;
using BasicWebApi.Domain.Entities;
using BasicWebApi.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BasicWebApi.Core.Repositories.implementations
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Contact>> GetContactsWithCompanyAndCountry()
        {
            return await _dbSet
                        .Include(s => s.Company)
                        .Include(s => s.Country)
                        .ToListAsync();
        }
    }
}
