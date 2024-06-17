using BasicWebApi.Core.Services.Interfaces;
using BasicWebApi.Domain.DTO;
using BasicWebApi.Domain.Entities;

namespace BasicWebApi.Core.Services.interfaces
{
    public interface IContactService : IBaseService<Contact, ContactDTO>
    {
        Task<IEnumerable<Contact>> GetContactsWithCompanyAndCountry();
        Task<IEnumerable<Contact>> FilterContact(int? countryId, int? companyId);
    }
}
