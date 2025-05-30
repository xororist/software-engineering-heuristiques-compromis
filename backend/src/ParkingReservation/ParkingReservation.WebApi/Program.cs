using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingReservation.Application.Dtos;
using ParkingReservation.Application.UsesCases;
using ParkingReservation.Application.UseCases.CancelReservation;
using ParkingReservation.Application.UsesCases.CheckInReservation;
using ParkingReservation.Domain;
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


builder.Services.AddDbContext<ReservationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAll,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ReservationDbContext>();

    if (!db.ParkingLots.Any())
    {
        var rows = new[] { 'A', 'B', 'C', 'D', 'E', 'F' };
        const int maxColumns = 10;

        var parkingSpots = new List<ParkingLot>();

        foreach (var row in rows)
        {
            for (int column = 0; column <= maxColumns; column++)
            {
                parkingSpots.Add(new ParkingLot(row, column, true));
            }
        }

        db.ParkingLots.AddRange(parkingSpots);
        db.SaveChanges();
    }
}

app.MapGet("/", () => "Hello World!");

app.MapGet("/available-places", (IGetAvailablePlaces query) =>
{
    var places = query.Handle();
    return Results.Ok(places);
});

app.MapOpenApi();
app.UseCors(allowAll);

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

