using System;

namespace GameStore.Api.Endpoints;
using GameStore.Api.Dtos;

public static class GamesEndpoints {
    const string GetGameEndpointName = "GetGameById";

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

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        //  GET /games
        group.MapGet("/", () => games);


        // GET /games/{id}
        group.MapGet("/{id}", (int id) => {
            var game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST /games
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
        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });
    }
}
