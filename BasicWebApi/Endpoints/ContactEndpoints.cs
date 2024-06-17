using BasicWebApi.Core.Exceptions;
using BasicWebApi.Core.Services.interfaces;
using BasicWebApi.Domain.DTO;
using BasicWebApi.Domain.Entities;
using System.Diagnostics.Metrics;

namespace BasicWebApi.Endpoints
{
    public static class ContactEndpoints
    {
        public static void MapContactEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/contacts", GetAllContacts);
            app.MapGet("/api/contacts/{id:int}", GetContactById);
            app.MapGet("/api/contacts/filter/{countryId:int}/{companyId:int}", FilterContacts);
            app.MapGet("/api/contacts/details", GetContactsWithCompanyAndCountry);
            app.MapGet("/api/contacts/page/{skip:int}/{take:int}", GetAllPageable);
            app.MapPost("/api/contacts", CreateContact);
            app.MapPut("/api/contacts/{id:int}", UpdateContact);
            app.MapDelete("/api/contacts/{id:int}", DeleteContact);
        }

        public static async Task<IResult> GetAllContacts(IContactService contactService)
        {
            var contacts = await contactService.GetAll();
            return Results.Ok(contacts);
        }
        public static async Task<IResult> GetAllPageable(int skip, int take, IContactService contactService)
        {
            var companies = await contactService.GetAllPageable(take, skip);
            return Results.Ok(companies);
        }

        public static async  Task<IResult> GetContactById(int id, IContactService contactService)
        {
            try
            {
                var contact = await contactService.GetById(id);
                return Results.Ok(contact);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex);
            }
        }

        public static async Task<IResult> FilterContacts(int? countryId, int? companyId, IContactService contactService)
        {
            var contacts = await contactService.FilterContact(countryId, companyId);
            return Results.Ok(contacts);
        }

        public static async Task<IResult> GetContactsWithCompanyAndCountry(IContactService contactService)
        {
            var contacts = await contactService.GetContactsWithCompanyAndCountry();
            return Results.Ok(contacts);
        }

        public static async Task<IResult> CreateContact(ContactDTO addContactDTO, IContactService contactService)
        {
            try
            {
                var id = await contactService.Create(addContactDTO);
                return Results.Created($"/api/contacts/{id}", id);
            }
            catch(Exception ex)
            {
                return Results.NotFound(ex);
            }
        }

        public static async Task<IResult> UpdateContact(int id, ContactDTO contact, IContactService contactService)
        {
            try
            {
                var updatedContact = await contactService.Update(id, contact);
                return Results.Ok(updatedContact);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex);
            }
        }

        public static async Task<IResult> DeleteContact(int id, IContactService contactService)
        {
            try
            {
                await contactService.Delete(id);
                return Results.Ok(false);
            }
            catch (Exception ex)
            {
                return Results.NotFound(ex);
            }
        }
    }
}
