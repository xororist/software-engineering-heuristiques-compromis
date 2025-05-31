namespace ParkingReservation.Domain.Query;

public interface IGetOngoingReservations
{
    Task<IEnumerable<Reservation>> GetOngoingReservationsAsync(Guid userId);
}
