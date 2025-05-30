using ParkingReservation.Application.UsesCases;
using ParkingReservation.Domain.Query;
using ParkingReservation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");
const string allowFrontend = "allowFrontend";


builder.Services.AddScoped<IQueryAvailablePlaces, ParkingRepository>();
builder.Services.AddScoped<IGetAvailablePlaces, GetAvailablePlaces>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowFrontend,
        policy  =>
        {
            policy.WithOrigins("http://localhost:3000");
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
app.UseCors(allowFrontend);

app.Run();
