namespace ParkingReservation.Application.Dtos;

public class ParkingLotDto(char row, int column, bool isAvailable)
{
    public char Row { get; set; } = row;
    public int Column { get; set; } = column;
    public bool IsAvailable { get; set; } = isAvailable;
}
