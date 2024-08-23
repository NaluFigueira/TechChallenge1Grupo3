using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Users.Infra.Context;

namespace PosTech.TechChallenge.Users.Api.Configuration;

public static class MigrationInitializer
{
    public static void ApplyMigrations(this WebApplication app)
    {
        Console.WriteLine("Applying migrations");
        using (var serviceScope = app.Services.CreateScope())
        {
            Console.WriteLine("Users...");
            var userServiceDb = serviceScope.ServiceProvider
                             .GetService<UserDbContext>();
            userServiceDb!.Database.Migrate();
        }
        Console.WriteLine("Done");
    }
}
