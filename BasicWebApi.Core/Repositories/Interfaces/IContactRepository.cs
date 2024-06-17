using BasicWebApi.Core.Interfaces;
using BasicWebApi.Domain.Entities;

namespace BasicWebApi.Core.Repositories.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<IEnumerable<Contact>> GetContactsWithCompanyAndCountry();
    }
}
