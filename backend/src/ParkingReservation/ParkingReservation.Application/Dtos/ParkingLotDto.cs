namespace ParkingReservation.Application.Dtos;

public class ParkingLotDto
{
    public char Row { get; set; }
    public int Column { get; set; }
    public bool IsAvailable { get; set; }
}
