namespace ParkingReservation.Application.Dtos;

public class MakeAReservationCommand
{
    public Guid UserId { get; set; }
    public char Row { get; set; }
    public int Column { get; set; }
    public bool IsAvailable { get; set; }
    public DateTime BeginningOfReservation { get; set; }
    public DateTime EndOfReservation { get; set; }
}