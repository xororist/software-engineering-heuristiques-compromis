using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain.Repositories;

namespace ParkingReservation.Application.UseCases.CancelReservation;

public interface ICancelAReservationHandler
{
    public Task HandleAsync(CancelAReservationCommand command);
}