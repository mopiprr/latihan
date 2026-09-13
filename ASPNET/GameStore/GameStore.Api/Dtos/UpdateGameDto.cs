using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

/// <summary>
/// Data transfer object used for updating an existing game.
/// Contains updated game details and validation constraints.
/// </summary>
/// <param name="Title">The updated title of the game. Required, max 50 characters.</param>
/// <param name="Description">The updated description of the game. Max 500 characters.</param>
/// <param name="GenreId">The updated identifier of the genre associated with the game. Must be between 1 and 50.</param>
/// <param name="Price">The updated retail price of the game. Must be a non-negative value.</param>
/// <param name="ReleaseDate">The updated release date of the game.</param>
public record class UpdateGameDto(
    [Required][StringLength(50)] string Title,
    [StringLength(500)] string Description,
    [Range(1, 50)] int GenreId,
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")] decimal Price,
    DateOnly ReleaseDate
);