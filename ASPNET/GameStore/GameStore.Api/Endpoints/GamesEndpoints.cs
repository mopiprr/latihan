using System;

namespace GameStore.Api.Endpoints;

using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Models;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Defines extension methods and route handlers for Game-related HTTP endpoints.
/// </summary>
public static class GamesEndpoints {
    /// <summary>
    /// Route name used for referencing the Get Game by ID endpoint when generating URLs.
    /// </summary>
    const string GetGameEndpointName = "GetGameById";

    /// <summary>
    /// Registers and maps all HTTP endpoints for game operations under the "/games" route group.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to configure routes on.</param>
    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET /games
        // Retrieves the full list of available games.
        group.MapGet("/",async  (GameStoreContext dbContext) => 
        await dbContext.Games.Include(game => game.Genre).Select(game => new GameSuummaryDto(
            game.Id,
            game.Title,
            game.Description,
            game.Genre!.Title,
            game.Price,
            game.ReleaseDate
        ))
        .AsNoTracking()
        .ToListAsync());

        // GET /games/{id}
        // Retrieves a single game by its unique identifier.
        // Returns 200 OK with the game if found, or 404 Not Found if the game does not exist.
        group.MapGet("/{id}",async (int id, GameStoreContext dbContext) => {
            var game = await dbContext.Games.FindAsync(id);

            return game is null ? Results.NotFound() : Results.Ok(
                new GameDetailsDto(
                    game.Id,
                    game.Title,
                    game.Description,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate
                )
            );
        })
        .WithName(GetGameEndpointName);

        // POST /games
        // Creates a new game based on the provided request body.
        // Generates an ID, appends the game to the collection, and returns 201 Created with a Location header.
        group.MapPost("/",async (CreateGameDto newGame, GameStoreContext dbContext) => {
            Game game = new()
            {
                Title = newGame.Title,
                Description = newGame.Description,
                GenreId = newGame.GenreId,
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            GameDetailsDto gameDto = new(
                game.Id,
                game.Title,
                game.Description,
                game.GenreId,
                game.Price,
                game.ReleaseDate
            );
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDto.Id }, gameDto);
        });

        // PUT /games/{id}
        // Updates an existing game matching the specified ID with new information.
        // Returns 200 OK if successfully updated, or 404 Not Found if no game matches the ID.
        group.MapPut("/{id}",async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) => {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Title = updatedGame.Title;
            existingGame.GenreId = updatedGame.GenreId;
            existingGame.Description = updatedGame.Description;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;

            await dbContext.SaveChangesAsync();

            return Results.Ok();
        });

        // DELETE /games/{id}
        // Deletes the game matching the specified ID from the store.
        // Returns 204 NoContent upon successful removal.
        group.MapDelete("/{id}",async (int id, GameStoreContext dbContext) => {
            await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });
    }
}
