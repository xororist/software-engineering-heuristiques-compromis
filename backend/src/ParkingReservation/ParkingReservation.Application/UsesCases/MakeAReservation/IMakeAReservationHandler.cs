using ParkingReservation.Application.Dtos;

namespace ParkingReservation.Application.UsesCases.MakeAReservation;

public interface IMakeAReservationHandler
{
    public Task HandleAsync(MakeAReservationCommand command);
}