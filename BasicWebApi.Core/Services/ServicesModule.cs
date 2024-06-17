using BasicWebApi.Core.Interfaces;
using BasicWebApi.Core.Repositories.implementations;
using BasicWebApi.Core.Repositories.Implementations;
using BasicWebApi.Core.Repositories.Interfaces;
using BasicWebApi.Core.Services.implementations;
using BasicWebApi.Core.Services.interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BasicWebApi.Core.Services
{
    public static class ServicesModule
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IContactRepository), typeof(ContactRepository));
            services.AddTransient(typeof(ICompanyRepository), typeof(CompanyRepository));
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IContactService, ContactService>();
        }
    }
}
