using ParkingReservation.Domain;
using ParkingReservation.Domain.Query;

namespace ParkingReservation.Infrastructure;

public class ParkingRepository : IQueryAvailablePlaces
{
    private static readonly List<char> Rows = ['A', 'B', 'C', 'D', 'E', 'F'];
    private const int MaxColumns = 10;

    private readonly Dictionary<(char Row, int Column), ParkingLot> _parkingGrid;

    public ParkingRepository()
    {
        _parkingGrid = new Dictionary<(char, int), ParkingLot>();

        foreach (var row in Rows)
        {
            for (var column = 0; column <= MaxColumns; column++)
            {
                _parkingGrid[(row, column)] = new ParkingLot
                {
                    Row = row,
                    Column = column,
                    IsAvailable = true
                };
            }
        }
    }

    public IEnumerable<ParkingLot> GetAllPlaces() => _parkingGrid.Values;

    public IEnumerable<ParkingLot> GetAvailablePlaces()
    {
        return _parkingGrid.Values.Where(p => p.IsAvailable);
    }
}

