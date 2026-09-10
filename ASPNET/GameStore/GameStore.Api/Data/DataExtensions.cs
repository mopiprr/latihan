using System;
using GameStore.Api.Data;
using Microsoft.EntityFrameworkCore;
using GameStore.Api.Models;

namespace GameStore.Api.Data;

/// <summary>
/// Provides extension methods for database configuration, migration, and initial seeding.
/// </summary>
public static class DataExtensions
{
    /// <summary>
    /// Applies any pending Entity Framework Core migrations to the database automatically at application startup.
    /// Creates a dedicated service scope to resolve the <see cref="GameStoreContext"/> and execute the migration.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance used to access application services.</param>
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }

    /// <summary>
    /// Registers the SQLite database context (<see cref="GameStoreContext"/>) with the dependency injection container
    /// and configures database seeding to insert default genres if the genres table is empty.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> used to register services.</param>
    public static void AddGameStoreDb(this WebApplicationBuilder builder)
    {
        // Connection string for the SQLite database file
        var connnString = builder.Configuration.GetConnectionString("GameStore") ?? "Data Source=GameStore.db";
        
        builder.Services.AddSqlite<GameStoreContext>(connnString, optionsAction: options => options.UseSeeding((context, _) =>
        {
            // Seed initial genres if none currently exist in the database
            if (!context.Set<Genre>().Any()) {
                context.Set<Genre>().AddRange(
                    new Genre { Title = "Action"},
                    new Genre { Title = "Racing" },
                    new Genre { Title = "Sports" },
                    new Genre { Title = "Adventure" }
                );
                context.SaveChanges();
                }
            })
        );
    }
}
