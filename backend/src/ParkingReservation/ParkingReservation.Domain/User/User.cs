namespace ParkingReservation.Domain.User;

public class User
{
    public Role Role { get; set; }

    public User(Role role)
    {
        this.Role = role;
    }
}