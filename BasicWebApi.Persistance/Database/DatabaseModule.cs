using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasicWebApi.Persistance.Database
{
    public static class DatabaseModule
    {
        public static void AddDatabase(this IServiceCollection collection, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            collection.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }

        //public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var connectionString = configuration.GetConnectionString("DefaultConnection");
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(connectionString));
        //    return services;
        //}

        public static async Task<IApplicationBuilder> UseDatabase(this IApplicationBuilder builder)
        {
            await builder.UseAutomaticMigrations();

            return builder;
        }
    }
}
