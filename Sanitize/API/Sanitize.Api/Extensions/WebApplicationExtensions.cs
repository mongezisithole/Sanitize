using Microsoft.EntityFrameworkCore;
using Sanitize.Data.Context;
using Sanitize.Data.Seeding;

namespace Sanitize.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void SeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<SensitiveContext>()!;

                //When using in memory db comment this line
                context.Database.Migrate(); // Apply any pending migration, create database if not exists

                Seed.SeedData(context);// Seed the database
            }
        }
    }
}
