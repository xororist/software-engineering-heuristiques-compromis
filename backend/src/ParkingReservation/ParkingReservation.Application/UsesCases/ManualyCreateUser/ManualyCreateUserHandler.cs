using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain.Repositories;
using ParkingReservation.Domain.User;

namespace ParkingReservation.Application.UsesCases.ManualyCreateUser;

public class ManualyCreateUserHandler(IUserRepository userRepository) : IManualyCreateUserHandler
{
    public async Task<Guid> HandleAsync(ManualyCreateUserCommand command)
    {
        var requester = await userRepository.GetByIdAsync(command.RequesterId);

        if (requester == null)
            throw new Exception("Requester not found.");

        if (requester.Role != Role.Secretary)
            throw new Exception("Only secretaries can create users.");

        var newUser = new User(command.Role);
        await userRepository.AddUserAsync(newUser);

        return newUser.Id;
    }
}