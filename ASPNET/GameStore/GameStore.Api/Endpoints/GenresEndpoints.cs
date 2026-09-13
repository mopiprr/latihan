using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

/// <summary>
/// Defines extension methods and route handlers for Genre-related HTTP endpoints.
/// </summary>
public static class GenresEndpoints
{
    /// <summary>
    /// Registers and maps all HTTP endpoints for genre operations under the "/genres" route group.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to configure routes on.</param>
    public static void MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/genres");

        // GET /genres
        // Retrieves the full list of available game genres.
        // Returns 200 OK with a collection of GenreDto objects.
        group.MapGet("/", async (GameStoreContext dbContext) => 
            await dbContext.Genres
            .Select(genre => new GenreDto(genre.Id, genre.Title))
            .AsNoTracking()
            .ToListAsync()
        );
    }
}
