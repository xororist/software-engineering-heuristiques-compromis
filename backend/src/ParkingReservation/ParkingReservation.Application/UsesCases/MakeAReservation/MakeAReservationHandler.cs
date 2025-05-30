using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Repositories;
using ParkingReservation.Domain.User;

namespace ParkingReservation.Application.UsesCases.MakeAReservation;

public class MakeAReservationHandler(
    IReservationRepository repository, 
    IUserRepository userRepository,
    IParkingLotRepository parkingLotRepository)
    : IMakeAReservationHandler
{
    public async Task HandleAsync(MakeAReservationCommand command)
    {
        var user = await userRepository.GetByIdAsync(command.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var parkingLot = await parkingLotRepository.GetByCoordinatesAsync(command.Row, command.Column);
        if (parkingLot == null)
        {
            throw new Exception("Parking lot not found");
        }

        bool isAvailable = await repository.IsParkingLotAvailableAsync(
            parkingLot.Id, 
            command.BeginningOfReservation, 
            command.EndOfReservation);

        if (!isAvailable)
        {
            throw new Exception("Ce parking est déjà réservé pour la période demandée");
        }

        var reservation = new Reservation(user, parkingLot, command.BeginningOfReservation, command.EndOfReservation);
        await repository.AddReservationAsync(reservation);
    }
}