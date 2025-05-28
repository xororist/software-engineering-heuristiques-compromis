using ParkingReservation.Domain;

namespace ParkingReservation.Application.Dtos;

public class CheckInAReservationCommand
{
    public Guid userId { get; set; }
    public char Row { get; set; }
    public int Column { get; set; }
    
    public DateTime CheckInTime { get; set; }
}