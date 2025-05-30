using Microsoft.EntityFrameworkCore;
using ParkingReservation.Domain.Repositories;
using ParkingReservation.Domain.User;

namespace ParkingReservation.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly ReservationDbContext _dbContext;

    
    public UserRepository(ReservationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
    
    public void AddUser(User user)
    {
        _dbContext.Users.Add(user);
    }
}