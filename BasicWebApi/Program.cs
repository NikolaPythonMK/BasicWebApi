using BasicWebApi.Core.Services;
using BasicWebApi.Endpoints;
using BasicWebApi.Persistance.Database;
using BasicWebApi.Web.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title="Swagger APIs", Version="v1" });
});

var app = builder.Build();

await app.UseDatabase();

app.MapCountryEndpoints();
app.MapContactEndpoints();
app.MapCompanyEndpoints();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasicWebApi");
    c.RoutePrefix = "";
});

app.Run();