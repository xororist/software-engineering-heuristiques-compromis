using Microsoft.EntityFrameworkCore;
using ParkingReservation.Domain;
using ParkingReservation.Domain.User;

namespace ParkingReservation.Infrastructure;

public class ReservationDbContext : DbContext
{
    public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<ParkingLot> ParkingLots => Set<ParkingLot>();
    public DbSet<Reservation> Reservations => Set<Reservation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Role)
                .HasConversion<string>() // Stocker l'enum Role en string dans la DB
                .IsRequired();
        });

        modelBuilder.Entity<ParkingLot>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Row)
                .IsRequired()
                .HasMaxLength(1);
            entity.Property(p => p.Column).IsRequired();
            entity.Property(p => p.IsAvailable).IsRequired();
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(r => r.ParkingLot)
                .WithMany()
                .HasForeignKey("ParkingLotId")
                .OnDelete(DeleteBehavior.SetNull);

            entity.Property(r => r.BeginningOfReservation).IsRequired();
            entity.Property(r => r.EndOfReservation).IsRequired();
            entity.Property(r => r.HasBeenConfirmed).IsRequired();
        });
    }
}