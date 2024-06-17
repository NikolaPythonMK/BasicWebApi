using BasicWebApi.Core.Exceptions;
using BasicWebApi.Core.Interfaces;
using BasicWebApi.Core.Repositories.Interfaces;
using BasicWebApi.Core.Services.interfaces;
using BasicWebApi.Core.Validations;
using BasicWebApi.Domain.DTO;
using BasicWebApi.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BasicWebApi.Core.Services.implementations
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CountryService> _logger;

        public CountryService(IRepository<Country> countryRepository,
                              ICompanyRepository companyRepository,
                              ILogger<CountryService> logger)
        {
            _countryRepository = countryRepository;
            _companyRepository = companyRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<Country>> GetAll()
        {
            return await _countryRepository.GetAll();
        }

        public async Task<Country> GetById(int id)
        {
            return await _countryRepository.GetById(id) ?? throw new CountryNotFoundException(id);
        }

        public async Task<Dictionary<string, int>> GetCompanyStatisticsByCountryId(int countryId)
        {
            IEnumerable<Company> companies = await _companyRepository.GetAllWithInclude();

            return companies.Select(c => new
            {
                CompanyName = c.CompanyName,
                NumberOfContacts = c.Contacts.Count(e => e.CountryId == countryId)
            })
            .Where(stat => stat.NumberOfContacts > 0) //Ignore the companies that have 0 employees
            .ToDictionary(stat => stat.CompanyName, stat => stat.NumberOfContacts);
        }

        public async Task<int> Create(CountryDTO addCountry)
        {
            ArgumentNullException.ThrowIfNull(addCountry, nameof(addCountry));

            var validator = new CountryValidation();
            var results = await validator.ValidateAsync(addCountry);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                    _logger.LogError($"Error: {failure.ErrorMessage}");

                throw new Exception(string.Join(",", results.Errors.Select(s => s.ErrorMessage)));
            }

            Country country = new Country
            {
                CountryName = addCountry.CountryName
            };

            return await _countryRepository.Add(country);
        }

        public async Task<int> Delete(int id)
        {
            Country country = await _countryRepository.GetById(id) ?? throw new CountryNotFoundException(id);
            await _countryRepository.Delete(country.Id);
            _logger.LogInformation("Country with ID: {id} deleted.", id);

            return id;
        }

        public async Task<int> Update(int id, CountryDTO countryDTO)
        {
            ArgumentNullException.ThrowIfNull(countryDTO, nameof(countryDTO));

            var validator = new CountryValidation();
            var results = await validator.ValidateAsync(countryDTO);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                    _logger.LogError($"Error: {failure.ErrorMessage}");

                throw new Exception(string.Join(",", results.Errors.Select(s => s.ErrorMessage)));
            }

            var country = await _countryRepository.GetById(id);
            country.CountryName = countryDTO.CountryName;

            var updatedId = await _countryRepository.Update(country);
            _logger.LogInformation("Country with ID: {id} updated. New value: {companyName}", country.Id, country.CountryName);
            return updatedId;
        }

        public async Task<IEnumerable<Country>> GetAllPageable(int take, int skip)
        {
            return await _countryRepository.GetAllPageable(take, skip);
        }
    }
}
