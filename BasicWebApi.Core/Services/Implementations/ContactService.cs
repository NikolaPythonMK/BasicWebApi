using BasicWebApi.Core.Exceptions;
using BasicWebApi.Core.Interfaces;
using BasicWebApi.Core.Repositories.implementations;
using BasicWebApi.Core.Repositories.Interfaces;
using BasicWebApi.Core.Services.interfaces;
using BasicWebApi.Core.Validations;
using BasicWebApi.Domain.DTO;
using BasicWebApi.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BasicWebApi.Core.Services.implementations
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Company> _companyRepository;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IContactRepository contactRepository,
                              IRepository<Country> countryRepository,
                              IRepository<Company> companyRepository,
                              ILogger<ContactService> logger)
        {
            _contactRepository = contactRepository;
            _countryRepository = countryRepository;
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task<Contact> GetById(int id)
        {
            return await _contactRepository.GetById(id) ?? throw new ContactNotFoundException(id);
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _contactRepository.GetAll();
        }

        public async Task<IEnumerable<Contact>> GetContactsWithCompanyAndCountry()
        {
            return await _contactRepository.GetContactsWithCompanyAndCountry();
        }

        public async Task<IEnumerable<Contact>> FilterContact(int? countryId, int? companyId)
        {
            IEnumerable<Contact> contacts = await _contactRepository.GetAll();
            
            if (!countryId.HasValue && !companyId.HasValue)
                return contacts;
            else if (countryId.HasValue && !companyId.HasValue)
                return contacts.Where(c => c.CountryId == countryId).ToList();
            else if (!countryId.HasValue && companyId.HasValue)
                return contacts.Where(c => c.CompanyId == companyId).ToList();
            else
                return contacts.Where(c => c.CountryId == countryId && c.CompanyId == companyId).ToList();
        }

        public async Task<int> Create(ContactDTO addContact)
        {
            ArgumentNullException.ThrowIfNull(nameof(addContact));

            var validator = new ContactValidation();
            var results = await validator.ValidateAsync(addContact);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                    _logger.LogError($"Error: {failure.ErrorMessage}");

                throw new Exception(string.Join(",", results.Errors.Select(s => s.ErrorMessage)));
            }

            Country country = await _countryRepository.GetById(addContact.CountryId) ?? throw new CountryNotFoundException(addContact.CountryId);
            Company company = await _companyRepository.GetById(addContact.CompanyId) ?? throw new CompanyNotFoundException(addContact.CompanyId);

            Contact contact = new Contact
            {
                ContactName = addContact.ContactName,
                CompanyId = addContact.CompanyId,
                CountryId = addContact.CountryId,
            };
            
            return await _contactRepository.Add(contact);
        }

        public async Task<int> Delete(int id)
        {
            Contact contact = await _contactRepository.GetById(id) ?? throw new ContactNotFoundException(id);
            await _contactRepository.Delete(contact.Id);
            _logger.LogInformation("Contact with ID: {id} deleted.", id);

            return id;
        }

        public async Task<int> Update(int id, ContactDTO contactDTO)
        {
            ArgumentNullException.ThrowIfNull(nameof(contactDTO));

            var validator = new ContactValidation();
            var results = await validator.ValidateAsync(contactDTO);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                    _logger.LogError($"Error: {failure.ErrorMessage}");

                throw new Exception(string.Join(",", results.Errors.Select(s => s.ErrorMessage)));
            }

            var contact = await _contactRepository.GetById(id);
            contact.ContactName = contactDTO.ContactName;
                
            Country country = await _countryRepository.GetById(contact.CountryId) ?? throw new CountryNotFoundException(contact.CountryId);
            Company company = await _companyRepository.GetById(contact.CompanyId) ?? throw new CompanyNotFoundException(contact.CompanyId);

            contact.CompanyId = country.Id;
            contact.CompanyId = company.Id;

            _logger.LogInformation("Contact with ID: {id} updated", contact.Id);
            var updatedId =  await _contactRepository.Update(contact);

            return updatedId;
        }

        public async Task<IEnumerable<Contact>> GetAllPageable(int take, int skip)
        {
            return await _contactRepository.GetAllPageable(take, skip);
        }
    }
}
