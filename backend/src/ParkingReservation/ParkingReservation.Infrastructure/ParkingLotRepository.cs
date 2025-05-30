using Microsoft.EntityFrameworkCore;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Repositories;

namespace ParkingReservation.Infrastructure;

public class ParkingLotRepository:IParkingLotRepository
{
    private readonly ReservationDbContext _dbContext;

    public ParkingLotRepository(ReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ParkingLot?> GetByCoordinatesAsync(char row, int column)
    {
        return await _dbContext.ParkingLots
            .FirstOrDefaultAsync(p => p.Row == row && p.Column == column);
    }
    public async Task<bool> IsParkingLotAvailableAsync(int parkingLotId, DateTime start, DateTime end)
    {
        // Vérifier qu'il n'y a pas de réservation qui chevauche la période demandée
        return !await _dbContext.Reservations
            .AnyAsync(r => 
                r.ParkingLotId == parkingLotId &&
                r.BeginningOfReservation < end &&
                r.EndOfReservation > start &&
                !r.HasBeenCancelled);
    }
    
}