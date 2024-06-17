using BasicWebApi.Core.Exceptions;
using BasicWebApi.Core.Interfaces;
using BasicWebApi.Core.Services.interfaces;
using BasicWebApi.Core.Validations;
using BasicWebApi.Domain.DTO;
using BasicWebApi.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BasicWebApi.Core.Services.implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> _companyRepository;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(IRepository<Company> companyRepository, ILogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }
        public async Task<Company> GetById(int id)
        {
            return await _companyRepository.GetById(id) ?? throw new CompanyNotFoundException(id);
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _companyRepository.GetAll();
        }

        public async Task<int> Create(CompanyDTO addCompany)
        {
            ArgumentNullException.ThrowIfNull(addCompany, nameof(addCompany));

            var validator = new CompanyValidation();
            var results = await validator.ValidateAsync(addCompany);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                    _logger.LogError($"Error: {failure.ErrorMessage}");

                throw new Exception(string.Join(",", results.Errors.Select(s => s.ErrorMessage)));
            }

            Company company = new Company()
            {
                CompanyName = addCompany.CompanyName,
            };

            return await _companyRepository.Add(company);
        }

        public async Task<int> Update(int id, CompanyDTO companyDTO)
        {
            ArgumentNullException.ThrowIfNull(companyDTO, nameof(companyDTO));

            var validator = new CompanyValidation();
            var results = await validator.ValidateAsync(companyDTO);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                    _logger.LogError($"Error: {failure.ErrorMessage}");

                throw new Exception(string.Join(",", results.Errors.Select(s => s.ErrorMessage)));
            }

            var company = await _companyRepository.GetById(id);

            company.CompanyName = companyDTO.CompanyName;

            _logger.LogInformation("Company with ID: {id} updated. New value: {companyName}", company.Id, company.CompanyName);
            var updatedId =  await _companyRepository.Update(company);

            return updatedId;
        }

        public async Task<int> Delete(int id)
        {
            Company company = await _companyRepository.GetById(id) ?? throw new CompanyNotFoundException(id);
            await _companyRepository.Delete(company.Id);
            _logger.LogInformation("Company with ID: {id} deleted.", id);

            return id;
        }

        public async Task<IEnumerable<Company>> GetAllPageable(int take, int skip)
        {
            return await _companyRepository.GetAllPageable(take, skip);
        }
    }
}
