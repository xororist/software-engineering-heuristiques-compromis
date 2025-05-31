using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingReservation.Application.Dtos;
using ParkingReservation.Application.UsesCases;
using ParkingReservation.Application.UseCases.CancelReservation;
using ParkingReservation.Application.UsesCases.CheckInReservation;
using ParkingReservation.Application.UsesCases.GetOngoingReservationsAsSecretary;
using ParkingReservation.Domain;
using ParkingReservation.Application.UsesCases.MakeAReservation;
using ParkingReservation.Domain.Query;
using ParkingReservation.Domain.Repositories;
using ParkingReservation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
const string allowAll = "allowAll";

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IQueryAvailablePlaces, ParkingRepository>();
builder.Services.AddScoped<IGetAvailablePlaces, GetAvailablePlaces>();
builder.Services.AddScoped<IMakeAReservationHandler, MakeAReservationHandler>();
builder.Services.AddScoped<ICancelAReservationHandler, CancelAReservationHandler>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IParkingLotRepository, ParkingLotRepository>();
builder.Services.AddScoped<IGetOngoingReservations, GetOngoingReservations>();
builder.Services.AddScoped<IGetOngoingReservationsHandler, GetOngoingReservationsHandler>();

builder.Services.AddScoped<ICheckInAReservationHandler, CheckInAReservationHandler>();
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ReservationDbContext>();
    db.Database.Migrate();
    if (!db.ParkingLots.Any())
    {
        var rows = new[] { 'A', 'B', 'C', 'D', 'E', 'F' };
        const int maxColumns = 10;

        var parkingSpots = new List<ParkingLot>();

        foreach (var row in rows)
        {
            for (int column = 0; column <= maxColumns; column++)
            {
                parkingSpots.Add(new ParkingLot(row, column));
            }
        }

        db.ParkingLots.AddRange(parkingSpots);
        db.SaveChanges();
    }
}

app.MapGet("/", () => "Hello World!");

app.MapGet("/available-places", async (
    [FromQuery] DateTime date, 
    [FromServices] IGetAvailablePlaces getAvailablePlaces) =>
{
    try
    {
        var utcDate = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        var result = await getAvailablePlaces.Handle(utcDate);
        return Results.Ok(result);
    }
    catch (Exception e)
    {
        return Results.Problem("Erreur interne survenue : " + e.Message);
    }
});





app.UseCors(allowAll);

app.MapPost("/make-reservation",
    async ([FromBody] MakeAReservationCommand command, [FromServices] IMakeAReservationHandler query) =>
    {
        try
        {
            await query.HandleAsync(command);
            return Results.Ok();
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.StackTrace);
        }
    });

app.MapPost("/check-in",
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

app.MapGet("/ongoing-reservations/{userId:guid}", async (
    Guid userId,
    [FromServices] IGetOngoingReservationsHandler handler) =>
{
    var result = await handler.HandleAsync(userId);
    return Results.Ok(result);
});

app.Run();

