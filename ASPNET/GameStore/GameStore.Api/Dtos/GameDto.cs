namespace GameStore.Api.Dtos;

public record class GameDto (
    int Id,
    string Title,
    string Description,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);