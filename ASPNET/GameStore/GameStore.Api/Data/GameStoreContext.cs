using System;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

/// <summary>
/// Represents the Entity Framework Core database context for the Game Store application,
/// providing access to query and save instances of <see cref="Game"/> and <see cref="Genre"/>.
/// </summary>
/// <param name="options">The options to be used by the <see cref="DbContext"/>.</param>
public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets the database set for querying and saving <see cref="Game"/> entities.
    /// </summary>
    public DbSet<Game> Games => Set<Game>();

    /// <summary>
    /// Gets the database set for querying and saving <see cref="Genre"/> entities.
    /// </summary>
    public DbSet<Genre> Genres => Set<Genre>();
}
