using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Repositories;
using ParkingReservation.Domain.User;

namespace ParkingReservation.Application.UseCases.CancelReservation;

public class CancelAReservationHandler(IReservationRepository repository, IUserRepository userRepository) : ICancelAReservationHandler
{
    public async Task HandleAsync(CancelAReservationCommand command)
    {
        var reservation = await repository.GetByIdAsync(command.ReservationId);
        if (reservation == null)
            throw new Exception("No reservation corresponding to this id :" + command.ReservationId);

        var user = await userRepository.GetByIdAsync(command.UserId);
        if (user == null)
            throw new Exception("User not found: " + command.UserId);

        var isOwner = reservation.User.Id == command.UserId;
        var isSecretary = user.Role == Role.Secretary;
        
        if (!isSecretary)
        {
            if (!isOwner)
                throw new Exception("You can only cancel your own reservations unless you are a secretary.");
            
            if (DateTime.UtcNow >= reservation.BeginningOfReservation)
                throw new Exception("You cannot cancel a reservation that has already started.");
    
            if (reservation.HasBeenConfirmed)
                throw new Exception("You cannot cancel a reservation that has already been confirmed.");
        }
        else
        {
            if (reservation.HasBeenConfirmed)
                throw new Exception("Secretaries cannot cancel confirmed reservations.");
        }

        await repository.CancelReservationAsync(command.ReservationId);
    }
}