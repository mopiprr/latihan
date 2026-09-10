using System;

namespace GameStore.Api;

/// <summary>
/// Represents a game genre entity stored in the database.
/// </summary>
public class Genre
{
    /// <summary>
    /// Gets or sets the primary key for the genre.
    /// </summary>
    public int Id { get; set;}
    
    /// <summary>
    /// Gets or sets the name/title of the genre.
    /// </summary>
    public required string Title { get; set;}

}
