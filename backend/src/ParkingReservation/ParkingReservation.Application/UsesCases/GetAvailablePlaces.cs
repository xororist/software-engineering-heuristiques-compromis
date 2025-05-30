using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain.Query;

namespace ParkingReservation.Application.UsesCases;

public interface IGetAvailablePlaces
{
    IEnumerable<ParkingLotDto> Handle();
}

public class GetAvailablePlaces : IGetAvailablePlaces
{
    private readonly IQueryAvailablePlaces _repository;

    public GetAvailablePlaces(IQueryAvailablePlaces repository)
    {
        _repository = repository;
    }

    public IEnumerable<ParkingLotDto> Handle()
    {
        return _repository.GetAvailablePlaces()
            .Select(p => new ParkingLotDto
            {
                Row = p.Row,
                Column = p.Column,
                IsAvailable = p.IsAvailable
            });
    }
}