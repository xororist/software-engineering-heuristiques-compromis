namespace ParkingReservation.Domain.Repositories;

public interface IParkingLotRepository
{
    Task<ParkingLot?> GetByCoordinatesAsync(char row, int column);
}