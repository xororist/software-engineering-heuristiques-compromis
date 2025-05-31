using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Query;
using ParkingReservation.Domain.Repositories;

namespace ParkingReservation.Application.UsesCases;

public interface IGetAvailablePlaces
{
    Task<IEnumerable<ParkingLotWithAvailabilityDto>> Handle(DateTime date);
}


public class GetAvailablePlaces(IReservationRepository reservationRepository) : IGetAvailablePlaces
{
    public async Task<IEnumerable<ParkingLotWithAvailabilityDto>> Handle(DateTime date)
    {
        // 1. Récupérer les places réservées à cette date
        var reserved = await reservationRepository.GetReservedCoordinatesAtDateAsync(date);
        var reservedSet = new HashSet<(char row, int col)>(reserved);

        // 2. Construire toutes les places possibles
        var rows = new[] { 'A', 'B', 'C', 'D', 'E', 'F' };
        const int maxColumns = 10;

        var all = new List<ParkingLotWithAvailabilityDto>();
        foreach (var row in rows)
        {
            for (int col = 0; col <= maxColumns; col++)
            {
                all.Add(new ParkingLotWithAvailabilityDto
                {
                    Row = row,
                    Column = col,
                    IsAvailable = !reservedSet.Contains((row, col))
                });
            }
        }

        return all;
    }
}
