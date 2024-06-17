using BasicWebApi.Core.Repositories.Interfaces;
using BasicWebApi.Domain.Entities;
using BasicWebApi.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebApi.Core.Repositories.Implementations
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Company>> GetAllWithInclude()
        {
            return await _dbSet.Include(s => s.Contacts).ToListAsync();
        }
    }
}
