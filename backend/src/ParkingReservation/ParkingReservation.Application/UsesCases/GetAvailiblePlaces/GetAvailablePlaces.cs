using ParkingReservation.Application.Dtos;
using ParkingReservation.Domain;
using ParkingReservation.Domain.Query;
using ParkingReservation.Domain.Repositories;

namespace ParkingReservation.Application.UsesCases;

public interface IGetAvailablePlaces
{
    Task<IEnumerable<ParkingLotDto>> Handle();
}

public class GetAvailablePlaces(IReservationRepository repository) : IGetAvailablePlaces
{
    public async Task<IEnumerable<ParkingLotDto>> Handle()
    {
        var result = await repository.FetchAvailablePlacesAsync();
        
        
        return ParkingLotMapper.ParkingLotListToDtos(result);
    }
}