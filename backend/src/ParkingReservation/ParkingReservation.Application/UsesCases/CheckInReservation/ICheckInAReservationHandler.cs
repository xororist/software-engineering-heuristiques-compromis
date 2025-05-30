using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Repositories;

namespace ParkingReservation.Application.UsesCases.CheckInReservation;

public interface ICheckInAReservationHandler
{
   public Task HandleAsync(CheckInAReservationCommand command);
}