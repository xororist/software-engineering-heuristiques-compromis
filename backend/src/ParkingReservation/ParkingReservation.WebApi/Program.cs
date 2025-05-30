using ParkingReservation.Application.Dtos;
using ParkingReservation.Application.UsesCases;
using ParkingReservation.Application.UseCases.CancelReservation;
using ParkingReservation.Application.UsesCases.CheckInReservation;
using ParkingReservation.Domain.Query;
using ParkingReservation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
const string allowAll = "allowAll";

builder.Services.AddScoped<IQueryAvailablePlaces, ParkingRepository>();
builder.Services.AddScoped<IGetAvailablePlaces, GetAvailablePlaces>();
builder.Services.AddScoped<ICancelAReservationHandler, CancelAReservationHandler>();


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

app.MapPost("/cancel-reservation", async (CancelAReservationCommand command, ICancelAReservationHandler handler) =>
{
    try
    {
        await handler.HandleAsync(command);
        return Results.Ok("Reservation cancelled successfully.");
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});

app.Run();
