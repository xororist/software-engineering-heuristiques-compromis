using Npgsql;
using ParkingReservation.Application.Dtos;
using ParkingReservation.Application.UsesCases.CheckInReservation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

using (var connection = new NpgsqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connexion à PostgreSQL réussie !");
    using (var command = new NpgsqlCommand("SELECT NOW()", connection))
    {
        var result = command.ExecuteScalar();
        Console.WriteLine($"Heure actuelle dans la base de données : {result}");
    }
}

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/check-in/", async (CheckInAReservationCommand command,ICheckInAReservationHandler query) =>
{
    try
    {
        await query.HandleAsync(command);
        return Results.Ok();
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.Run();
