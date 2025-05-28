namespace ParkingReservation.Domain;

public class ParkingLot
{
    public char Row { get; set; }
    public int Column { get; set; }

    private static readonly List<char> _ValidRows = ['A', 'B', 'C', 'D', 'E', 'F'];

    public bool IsValidParkingLot(char row, int column)
    {
        if(!_ValidRows.Contains(row) || column > 10 || column < 0)
            return false;
        return true;
    }
}