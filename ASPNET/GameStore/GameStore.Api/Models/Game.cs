using System;

namespace GameStore.Api.Models;

/// <summary>
/// Represents a game entity stored in the database.
/// </summary>
public class Game {
    /// <summary>
    /// Gets or sets the primary key for the game.
    /// </summary>
    public int Id { get; set;}

    /// <summary>
    /// Gets or sets the title of the game.
    /// </summary>
    public required string Title { get; set;}

    /// <summary>
    /// Gets or sets the navigation property for the associated <see cref="Genre"/>.
    /// </summary>
    public Genre? Genre { get; set;}

    /// <summary>
    /// Gets or sets the foreign key referencing the associated <see cref="Genre"/>.
    /// </summary>
    public int GenreId { get; set;}

    /// <summary>
    /// Gets or sets the description of the game.
    /// </summary>
    public string Description { get; set;}

    /// <summary>
    /// Gets or sets the retail price of the game.
    /// </summary>
    public decimal Price { get; set;}

    /// <summary>
    /// Gets or sets the release date of the game.
    /// </summary>
    public DateOnly ReleaseDate { get; set;}
}
