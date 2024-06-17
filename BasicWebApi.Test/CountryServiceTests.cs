using BasicWebApi.Core.Interfaces;
using BasicWebApi.Core.Repositories.Interfaces;
using BasicWebApi.Core.Services.implementations;
using BasicWebApi.Core.Services.interfaces;
using BasicWebApi.Core.Validations;
using BasicWebApi.Domain.DTO;
using BasicWebApi.Domain.Entities;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;


namespace BasicWebApi.Test
{
    public class CountryServiceTests
    {
        private readonly Mock<IRepository<Country>> _countryRepositoryMock;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly Mock<ILogger<CountryService>> _loggerMock;
        private readonly CountryService _countryService;

        public CountryServiceTests()
        {
            _countryRepositoryMock = new Mock<IRepository<Country>>();
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _loggerMock = new Mock<ILogger<CountryService>>();
            _countryService = new CountryService(_countryRepositoryMock.Object, _companyRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Create_ValidCountryDTO_ReturnsNewCountryId()
        {
            var newCountryDTO = new CountryDTO("New Country");
            var newCountryId = 1;
            _countryRepositoryMock.Setup(repo => repo.Add(It.IsAny<Country>())).ReturnsAsync(newCountryId);

            var result = await _countryService.Create(newCountryDTO);

            Assert.Equal(newCountryId, result);
        }

        [Fact]
        public async Task Create_InvalidCountryDTO_ThrowsException()
        {
            var newCountryDTO = new CountryDTO("");
          
            await Assert.ThrowsAsync<Exception>(() => _countryService.Create(newCountryDTO));
        }
    }
}
