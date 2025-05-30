namespace ParkingReservation.Domain.Repositories;

public interface IReservationRepository
{
    
    Task<Reservation?> GetByIdAsync(Guid id);
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByUserIdAsync(Guid userId);
    //Task<bool> ReservationIsCorrectAsync(Guid userId, char row, int column, DateTime checkInTime);
    Task AddReservationAsync(Reservation reservation);
    Task CheckInReservationAsync(Guid reservationId);
    Task CancelReservationAsync(Guid reservationId);
    Task<IEnumerable<ParkingLot>> FetchAvailablePlacesAsync();
    
    Task<bool> IsParkingLotAvailableAsync(int parkingLotId, DateTime start, DateTime end);

}