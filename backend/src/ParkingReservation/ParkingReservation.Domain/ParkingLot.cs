namespace ParkingReservation.Domain;

public class ParkingLot(char row, int column, bool isAvailable)
{
    public char Row { get; set; } = row;
    public int Column { get; set; } = column;
    public bool IsAvailable { get; set; } = isAvailable;

    private static readonly List<char> ValidRows = ['A', 'B', 'C', 'D', 'E', 'F'];

    public static bool IsValidParkingLot(char row, int column)
    {
        return ValidRows.Contains(row) && column is <= 10 and >= 0;
    }
}