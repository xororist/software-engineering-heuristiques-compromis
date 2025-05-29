using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Repositories;

namespace ParkingReservation.Application.UseCases.CancelReservation;

public class CancelAReservationHandler : ICancelAReservationHandler
{
    public async Task HandleAsync(IReservationRepository repository, CancelAReservationCommand command)
    {
        var reservation = await repository.GetByIdAsync(command.ReservationId);
        if (reservation == null)
            throw new Exception("No reservation corresponding to this id :" + command.ReservationId);

        if (reservation.User.Id != command.UserId)
            throw new Exception("You can only cancel your own reservations.");

        if (DateTime.Now >= reservation.BeginningOfReservation)
            throw new Exception("You cannot cancel a reservation that has already started.");

        await repository.CancelReservationAsync(command.ReservationId);
    }
}