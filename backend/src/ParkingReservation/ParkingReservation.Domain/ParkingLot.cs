namespace ParkingReservation.Domain;

public class ParkingLot
{
    public char Row { get; set; }
    public int Column { get; set; }
    public bool IsAvailable { get; set; }

    private static readonly List<char> ValidRows = ['A', 'B', 'C', 'D', 'E', 'F'];

    public bool IsValidParkingLot(char row, int column)
    {
        return ValidRows.Contains(row) && column is <= 10 and >= 0;
    }
}