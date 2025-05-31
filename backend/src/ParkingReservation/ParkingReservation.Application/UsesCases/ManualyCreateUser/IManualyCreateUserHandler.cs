using ParkingReservation.Application.Dtos;

namespace ParkingReservation.Application.UsesCases.ManualyCreateUser;

public interface IManualyCreateUserHandler
{
    Task<Guid> HandleAsync(ManualyCreateUserCommand command);
}