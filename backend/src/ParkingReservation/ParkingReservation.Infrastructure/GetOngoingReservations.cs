using Microsoft.EntityFrameworkCore;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Query;
using ParkingReservation.Domain.User;

namespace ParkingReservation.Infrastructure
{
    public class GetOngoingReservations : IGetOngoingReservations
    {
        private readonly ReservationDbContext _dbContext;

        public GetOngoingReservations(ReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Reservation>> GetOngoingReservationsAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);

            if (user is not { Role: Role.Secretary })
                return [];

            var now = DateTime.UtcNow;

            return await _dbContext.Reservations
                .Include(r => r.User)
                .Include(r => r.ParkingLot)
                .Where(r => r.BeginningOfReservation <= now
                            && r.EndOfReservation >= now
                            && !r.HasBeenCancelled)
                .ToListAsync();
        }
    }
}