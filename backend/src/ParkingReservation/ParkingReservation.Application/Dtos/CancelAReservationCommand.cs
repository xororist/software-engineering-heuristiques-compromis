using System;

namespace ParkingReservation.Application.Dtos;

public class CancelAReservationCommand
{
    public Guid ReservationId { get; set; }
    public Guid UserId { get; set; }
}