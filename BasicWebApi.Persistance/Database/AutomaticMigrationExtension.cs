using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BasicWebApi.Persistance.Database
{
    public static class AutomaticMigrationExtension
    {
        public static async Task<IApplicationBuilder> UseAutomaticMigrations(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            try
            {
                await context.Database.MigrateAsync();
            }
            catch
            {
                throw;
            }

            return applicationBuilder;
        }
    }
}
