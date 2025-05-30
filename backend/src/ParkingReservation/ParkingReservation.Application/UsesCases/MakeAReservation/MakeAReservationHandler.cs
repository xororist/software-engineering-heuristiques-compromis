using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Repositories;
using ParkingReservation.Domain.User;

namespace ParkingReservation.Application.UsesCases.MakeAReservation;

public class MakeAReservationHandler(IReservationRepository repository, IUserRepository userRepository)
    : IMakeAReservationHandler
{
    public async Task HandleAsync(MakeAReservationCommand command)
    {
        var user = await userRepository.GetByIdAsync(command.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var parkingLot = new ParkingLot(command.Row, command.Column, command.IsAvailable);
        var reservation = new Reservation(user, parkingLot, command.BeginningOfReservation, command.EndOfReservation);

        repository.AddReservationAsync(reservation);
    }
}