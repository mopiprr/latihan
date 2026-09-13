namespace GameStore.Api.Dtos;

/// <summary>
/// Data transfer object representing summary information of a game returned in list queries to clients.
/// </summary>
/// <param name="Id">The unique identifier of the game.</param>
/// <param name="Title">The title of the game.</param>
/// <param name="Description">A detailed description of the game.</param>
/// <param name="Genre">The genre name associated with the game.</param>
/// <param name="Price">The retail price of the game.</param>
/// <param name="ReleaseDate">The official release date of the game.</param>
public record class GameSuummaryDto (
    int Id,
    string Title,
    string Description,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);