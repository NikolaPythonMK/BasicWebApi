using BasicWebApi.Core.Services.Interfaces;
using BasicWebApi.Domain.DTO;
using BasicWebApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebApi.Core.Services.interfaces
{
    public interface ICountryService : IBaseService<Country, CountryDTO>
    {
        Task<Dictionary<string, int>> GetCompanyStatisticsByCountryId(int countryId);
    }
}
