using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Repositories;

namespace ParkingReservation.Application.UsesCases.CheckInReservation;

public class CheckInAReservationHandler(IReservationRepository repository): ICheckInAReservationHandler
{
    public async Task HandleAsync(CheckInAReservationCommand command)
    {
        Reservation? reservation = await repository.GetByUserIdAsync(command.userId);
        
        if (reservation == null) throw new Exception("You don't have any reservation");
        
        if (command.Row != reservation.ParkingLot.Row || command.Column != reservation.ParkingLot.Column)
        {
            throw new Exception("You reservation isn't for that place ");
        } 
        if (reservation.EndOfReservation > command.CheckInTime &&
            reservation.BeginningOfReservation < command.CheckInTime)
        {
            if (command.CheckInTime.TimeOfDay < new TimeSpan(10, 0, 0))
            {
                 repository.CheckInReservationAsync(reservation.Id);
            }
            else
            {
                throw new Exception("Check-in time must be before 10:00 AM");
            }
        }

        throw new Exception("Check-in time must be the same day as reservation");

    }
}