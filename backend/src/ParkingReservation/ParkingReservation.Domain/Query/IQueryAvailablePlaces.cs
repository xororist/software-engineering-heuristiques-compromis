namespace ParkingReservation.Domain.Query;

public interface IQueryAvailablePlaces
{
    IEnumerable<ParkingLot> GetAvailablePlaces();
}