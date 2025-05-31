using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain.Query;

namespace ParkingReservation.Application.UsesCases.GetOngoingReservationsAsSecretary;

public interface IGetOngoingReservationsHandler
{
    Task<IEnumerable<ReservationDto>> HandleAsync(Guid userId);
}

public class GetOngoingReservationsHandler : IGetOngoingReservationsHandler
{
    private readonly IGetOngoingReservations _repository;

    public GetOngoingReservationsHandler(IGetOngoingReservations repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ReservationDto>> HandleAsync(Guid userId)
    {
        var reservations = await _repository.GetOngoingReservationsAsync(userId);

        return reservations.Select(r => new ReservationDto
        {
            Id = r.Id,
            UserId = r.UserId,
            UserRole = r.User.Role.ToString(),
            Row = r.ParkingLot?.Row ?? '?',
            Column = r.ParkingLot?.Column ?? -1,
            Start = r.BeginningOfReservation,
            End = r.EndOfReservation,
            IsConfirmed = r.HasBeenConfirmed
        });
    }
}