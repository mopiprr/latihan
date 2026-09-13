namespace GameStore.Api.Dtos;

/// <summary>
/// Data transfer object representing a game genre returned to API clients.
/// </summary>
/// <param name="Id">The unique identifier of the genre.</param>
/// <param name="Title">The name or title of the genre.</param>
public record class GenreDto(int Id, string Title);