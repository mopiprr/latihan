using GameStore.Api;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;

// Initialize the web application builder
var builder = WebApplication.CreateBuilder(args);

// Register application services
builder.Services.AddValidation(); // Adds support for endpoint argument validation
builder.AddGameStoreDb();         // Registers the SQLite database context and seeds initial genres

// Build the application pipeline
var app = builder.Build();

// Configure HTTP request pipeline and endpoints
app.MapGamesEndpoints(); // Maps all minimal API endpoints for /games
app.MapGenresEndpoints(); // Maps all minimal API endpoints for /genres
app.MigrateDb();         // Applies any pending database migrations on startup
app.Run();               // Starts listening for incoming HTTP requests
