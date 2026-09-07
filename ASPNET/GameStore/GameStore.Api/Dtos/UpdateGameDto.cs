using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class UpdateGameDto(
    [Required][StringLength(50)] string Title,
    [StringLength(500)] string Description,
    [Required][StringLength(20)] string Genre,
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")] decimal Price,
    DateOnly ReleaseDate
);