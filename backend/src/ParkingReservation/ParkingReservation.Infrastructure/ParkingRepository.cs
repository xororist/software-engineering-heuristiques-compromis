using Microsoft.EntityFrameworkCore;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Query;

namespace ParkingReservation.Infrastructure;

public class ParkingRepository : IQueryAvailablePlaces
{
    private readonly ReservationDbContext _dbContext;

    public ParkingRepository(ReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<ParkingLot> GetAvailablePlaces()
    {
        return _dbContext.ParkingLots
            .AsNoTracking()
            .Where(p => p.IsAvailable)
            .ToList();
    }
}