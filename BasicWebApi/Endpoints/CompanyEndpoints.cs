using BasicWebApi.Core.Services.interfaces;
using BasicWebApi.Domain.DTO;
using BasicWebApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebApi.Web.Endpoints
{
    public static class CompanyEndpoints
    {
        public static void MapCompanyEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/company", GetAllCompanies);
            app.MapGet("/api/company/{id:int}", GetCountryById);
            app.MapGet("/api/company/page/{skip:int}/{take:int}", GetAllPageable);
            app.MapPost("/api/company", CreateCountry);
            app.MapPut("/api/company/{id:int}", UpdateCountry);
            app.MapDelete("/api/company/{id:int}", DeleteCountry);
        }

        public static async Task<IResult> GetAllCompanies(ICompanyService companyService)
        {
            var companies = await companyService.GetAll();
            return Results.Ok(companies);
        }

        public static async Task<IResult> GetAllPageable(int skip, int take, ICompanyService companyService)
        {
            var companies = await companyService.GetAllPageable(take, skip);
            return Results.Ok(companies);
        }

        public static async Task<IResult> GetCountryById(int id, ICompanyService companyService)
        {
            try
            {
                var company = await companyService.GetById(id);
                return Results.Ok(company);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

        public static async Task<IResult> CreateCountry(CompanyDTO addCompany, ICompanyService companyService)
        {
            try
            {
                var id = await companyService.Create(addCompany);
                return Results.Created($"/api/countries/{id}", id);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> UpdateCountry(int id, [FromBody] CompanyDTO company, ICompanyService companyService)
        {
            try
            {
                var updatedCountry = await companyService.Update(id, company);
                return Results.Ok(updatedCountry);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> DeleteCountry(int id, ICompanyService companyService)
        {
            try
            {
                await companyService.Delete(id);
                return Results.Ok(id);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex.Message);
            }
        }

    }
}
