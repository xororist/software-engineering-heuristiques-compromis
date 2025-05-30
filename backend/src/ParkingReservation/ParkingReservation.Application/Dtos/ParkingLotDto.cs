namespace ParkingReservation.Application.Dtos;

public class ParkingLotDto(char row, int column)
{
    public char Row { get; set; } = row;
    public int Column { get; set; } = column;
}
