using Microsoft.AspNetCore.Mvc;
using ParkingReservation.Application.Dtos;
using ParkingReservation.Application.UsesCases;
using ParkingReservation.Application.UsesCases.CheckInReservation;
using ParkingReservation.Application.UsesCases.MakeAReservation;
using ParkingReservation.Domain.Query;
using ParkingReservation.Domain.Repositories;
using ParkingReservation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
const string allowAll = "allowAll";

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQueryAvailablePlaces, ParkingRepository>();
builder.Services.AddScoped<IGetAvailablePlaces, GetAvailablePlaces>();
builder.Services.AddScoped<IMakeAReservationHandler, MakeAReservationHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAll,
        policy  =>
        {
            policy.WithOrigins("*");
        });
});

/*using (var connection = new NpgsqlConnection(connectionString))
{
    connection.Open();
    Console.WriteLine("Connexion à PostgreSQL réussie !");
    using (var command = new NpgsqlCommand("SELECT NOW()", connection))
    {
        var result = command.ExecuteScalar();
        Console.WriteLine($"Heure actuelle dans la base de données : {result}");
    }
}*/

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/available-places", (IGetAvailablePlaces query) =>
{
    var places = query.Handle();
    return Results.Ok(places);
});


app.MapOpenApi();
app.UseCors(allowAll);

app.MapPost("/make-reservation/",
    async ([FromBody] MakeAReservationCommand command, [FromServices] IMakeAReservationHandler query) =>
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

app.MapPost("/check-in/",
    async ([FromBody] CheckInAReservationCommand command, [FromServices] ICheckInAReservationHandler query) =>
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
