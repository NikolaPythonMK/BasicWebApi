using BasicWebApi.Core.Exceptions;
using BasicWebApi.Core.Services.interfaces;
using BasicWebApi.Domain.DTO;
using BasicWebApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebApi.Endpoints
{
    public static class CountryEndpoints
    {
       public static void MapCountryEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/countries", GetAllCountries);
            app.MapGet("/api/countries/{id:int}", GetCountryById);
            app.MapGet("/api/countries/{countryId:int}/statistics", GetCompanyStatisticsByCountryId);
            app.MapGet("/api/countries/page/{skip:int}/{take:int}", GetAllPageable);
            app.MapPost("/api/countries", CreateCountry);
            app.MapPut("/api/countries/{id:int}", UpdateCountry);
            app.MapDelete("/api/countries/{id:int}", DeleteCountry);
        }

        public static async Task<IResult> GetAllCountries(ICountryService countryService)
        {
            var countries = await countryService.GetAll();
            return Results.Ok(countries);
        }

        public static async Task<IResult> GetAllPageable(int skip, int take, ICountryService countryService)
        {
            var companies = await countryService.GetAllPageable(take, skip);
            return Results.Ok(companies);
        }

        public static async Task<IResult> GetCountryById(int id, ICountryService countryService)
        {
            try
            {
                var country = await countryService.GetById(id);
                return Results.Ok(country);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

        public static async Task<IResult> GetCompanyStatisticsByCountryId(int countryId, ICountryService countryService)
        {
            var statistics = await countryService.GetCompanyStatisticsByCountryId(countryId);
            return Results.Ok(statistics);
        }

        public static async Task<IResult> CreateCountry(CountryDTO addCountry, ICountryService countryService)
        {
            try
            {
                var id = await countryService.Create(addCountry);
                return Results.Created($"/api/countries/{id}", id);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> UpdateCountry(int id, CountryDTO country, ICountryService countryService)
        {
            try
            {
                var updatedCountry = await countryService.Update(id, country);
                return Results.Ok(updatedCountry);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> DeleteCountry(int id, ICountryService countryService)
        {
            try
            {
                await countryService.Delete(id);
                return Results.Ok(id);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

    }
}
