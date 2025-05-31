using ParkingReservation.Domain.User;

namespace ParkingReservation.Application.Dtos;

public class ManualyCreateUserCommand
{
    public Guid RequesterId { get; set; }
    public Role Role { get; set; }
}