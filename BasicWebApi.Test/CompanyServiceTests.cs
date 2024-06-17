using BasicWebApi.Core.Interfaces;
using BasicWebApi.Core.Services.implementations;
using BasicWebApi.Domain.DTO;
using BasicWebApi.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace BasicWebApi.Test
{
    public class CompanyServiceTests
    {
        private readonly Mock<IRepository<Company>> _companyRepositoryMock;
        private readonly Mock<ILogger<CompanyService>> _loggerMock;
        private readonly CompanyService _companyService;

        public CompanyServiceTests()
        {
            _companyRepositoryMock = new Mock<IRepository<Company>>();
            _loggerMock = new Mock<ILogger<CompanyService>>();
            _companyService = new CompanyService(_companyRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsCompany()
        {
            var companyId = 1;
            var expectedCompany = new Company { Id = companyId, CompanyName = "Test Company" };
            _companyRepositoryMock.Setup(repo => repo.GetById(companyId)).ReturnsAsync(expectedCompany);

            var result = await _companyService.GetById(companyId);

            Assert.NotNull(result);
            Assert.Equal(expectedCompany.Id, result.Id);
            Assert.Equal(expectedCompany.CompanyName, result.CompanyName);
        }

        [Fact]
        public async Task Create_EmptyCompanyName_ThrowsInvalidNameException()
        {
            var newCompanyDTO = new CompanyDTO("");

            await Assert.ThrowsAsync<Exception>(() => _companyService.Create(newCompanyDTO));
        }

    }
}