using ParkingReservation.Domain;
using ParkingReservation.Domain.Repositories;

namespace ParkingReservation.Infrastructure;

public class ReservationRepository: IReservationRepository
{
    List<Reservation> reservations = [];
    
    public Task<Reservation?> GetByIdAsync(Guid id)
    {
        var reservation = reservations.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(reservation);
    }

    public Task<IEnumerable<Reservation>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Reservation>>(reservations);
    }

    Task<Reservation> IReservationRepository.GetByUserIdAsync(Guid userId)
    {
        var userReservations = reservations.Where(r => r.User.Id == userId);
        return Task.FromResult<Reservation>(userReservations);    }

    public Task CheckInReservationAsync(Guid reservationId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Reservation>> GetByUserIdAsync(Guid userId)
    {
        
    }
   



    /*public Task<bool> ReservationIsCorrectAsync(Guid userId, char row, int column, DateTime checkInTime)
    {
        var reservation = reservations.FirstOrDefault(r =>
            r.User.Id == userId && r.ParkingLot.Row == row && r.ParkingLot.Column == column && r.D == checkInTime);
        return Task.FromResult(reservation != null);
    }

    public Task CheckInReservationAsync(Guid userId, char row, int column, DateTime checkInTime)
    {
        
*/
   
}