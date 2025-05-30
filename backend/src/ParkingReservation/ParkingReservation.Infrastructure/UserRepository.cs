using ParkingReservation.Domain.Repositories;
using ParkingReservation.Domain.User;

namespace ParkingReservation.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = [];

    public Task<User?> GetByIdAsync(Guid userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        return Task.FromResult(user);
    }

    public void AddUser(User user)
    {
        _users.Add(user);
    }
}