using Microsoft.EntityFrameworkCore;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Repositories;

namespace ParkingReservation.Infrastructure;

public class ReservationRepository: IReservationRepository
{
    private readonly ReservationDbContext _dbContext;

    public ReservationRepository(ReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Reservation?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Reservations
            .Include(r => r.User)
            .Include(r => r.ParkingLot)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return await _dbContext.Reservations
            .Include(r => r!.User)
            .Include(r => r!.ParkingLot)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByUserIdAsync(Guid userId)
    {
        return await _dbContext.Reservations
            .Include(r => r!.User)
            .Include(r => r!.ParkingLot)
            .FirstOrDefaultAsync(r => r != null && r.User.Id == userId);
    }

    public async Task CancelReservationAsync(Guid reservationId)
    {
        var reservation = await _dbContext.Reservations.FindAsync(reservationId);
        
        if (reservation != null)
        {
            // Supposant que HasBeenCancelled existe dans votre modèle
            reservation.HasBeenCancelled = true;
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task CheckInReservationAsync(Guid reservationId)
    {
        var reservation = await _dbContext.Reservations.FindAsync(reservationId);

        if (reservation == null) return;
        reservation.HasBeenConfirmed = true;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<ParkingLot>> FetchAvailablePlacesAsync()
    {
        var currentlyReservedParkingLotIds = await _dbContext.Reservations
            .Where(r => r!.BeginningOfReservation <= DateTime.UtcNow 
                        && r.EndOfReservation > DateTime.UtcNow
                        && !r.HasBeenCancelled)
            .Select(r => r!.ParkingLot!.Id)
            .ToListAsync();
        
        return await _dbContext.ParkingLots
            .Where(p => !currentlyReservedParkingLotIds.Contains(p.Id))
            .ToListAsync();
    }
}