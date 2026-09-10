using System;

namespace GameStore.Api.Endpoints;
using GameStore.Api.Dtos;

/// <summary>
/// Defines extension methods and route handlers for Game-related HTTP endpoints.
/// </summary>
public static class GamesEndpoints {
    /// <summary>
    /// Route name used for referencing the Get Game by ID endpoint when generating URLs.
    /// </summary>
    const string GetGameEndpointName = "GetGameById";

    /// <summary>
    /// In-memory list simulating a database of games.
    /// </summary>
    private static readonly List<GameDto> games = [
        new (1,
        "Game 1",
        "Description for Game 1",
        "Action",
        59.99m,
        new DateOnly(2023, 1, 15)),

        new (2,
        "Game 2",
        "Description for Game 2",
        "Adventure",
        49.99m,
        new DateOnly(2023, 2, 15)),
        
        new (3,
        "Game 3",
        "Description for Game 3",
        "RPG",
        39.99m,
        new DateOnly(2023, 3, 15))
    ];

    /// <summary>
    /// Registers and maps all HTTP endpoints for game operations under the "/games" route group.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to configure routes on.</param>
    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        // GET /games
        // Retrieves the full list of available games.
        group.MapGet("/", () => games);

        // GET /games/{id}
        // Retrieves a single game by its unique identifier.
        // Returns 200 OK with the game if found, or 404 Not Found if the game does not exist.
        group.MapGet("/{id}", (int id) => {
            var game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST /games
        // Creates a new game based on the provided request body.
        // Generates an ID, appends the game to the collection, and returns 201 Created with a Location header.
        group.MapPost("/", (CreateGameDto newGame) => {
            GameDto game = new(games.Count + 1,
            newGame.Title,
            newGame.Description,
            newGame.Genre,
            newGame.Price,
            newGame.ReleaseDate
            );
            games.Add(game);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        // PUT /games/{id}
        // Updates an existing game matching the specified ID with new information.
        // Returns 200 OK if successfully updated, or 404 Not Found if no game matches the ID.
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => {
            var index = games.FindIndex(game => id == game.Id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Title,
                updatedGame.Description,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.Ok();
        });

        // DELETE /games/{id}
        // Deletes the game matching the specified ID from the store.
        // Returns 204 NoContent upon successful removal.
        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
    }
}
