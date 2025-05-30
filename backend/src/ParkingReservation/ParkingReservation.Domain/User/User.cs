namespace ParkingReservation.Domain.User;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Role Role { get; set; }

    public User(Role role)
    {
        this.Role = role;
    }
}