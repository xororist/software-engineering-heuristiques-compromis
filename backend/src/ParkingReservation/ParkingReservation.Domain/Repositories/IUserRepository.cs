using AppUser = ParkingReservation.Domain.User.User;

namespace ParkingReservation.Domain.Repositories;

public interface IUserRepository
{
    Task<AppUser> GetByIdAsync(Guid userId);
    Task AddUserAsync(AppUser user);
}