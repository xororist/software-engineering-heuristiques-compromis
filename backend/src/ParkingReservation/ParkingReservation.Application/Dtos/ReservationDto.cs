namespace ParkingReservation.Application.Dtos;

public class ReservationDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserRole { get; set; }
    public char Row { get; set; }
    public int Column { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public bool IsConfirmed { get; set; }
}
