namespace ParkingReservation.Domain.User;

public class User
{
    public Guid Id { get; set; }
    public Role Role { get; set; }

    public User() {} 

    public User(Role role)
    {
        Id = Guid.NewGuid();
        Role = role;
    }
}