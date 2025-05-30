namespace ParkingReservation.Domain.Repositories;

public interface IReservationRepository
{
    
    Task<Reservation?> GetByIdAsync(Guid id);
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation> GetByUserIdAsync(Guid userId);
    //Task<bool> ReservationIsCorrectAsync(Guid userId, char row, int column, DateTime checkInTime);
    void CheckInReservationAsync(Guid reservationId);
    void CancelReservationAsync(Guid reservationId);
}