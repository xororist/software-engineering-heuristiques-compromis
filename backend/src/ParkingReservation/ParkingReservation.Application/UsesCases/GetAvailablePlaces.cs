using ParkingReservation.Domain;
using ParkingReservation.Domain.Query;

namespace ParkingReservation.Application.UsesCases;

public interface IGetAvailablePlaces
{
    IEnumerable<ParkingLot> Handle();
}

public class GetAvailablePlaces(IQueryAvailablePlaces query) : IGetAvailablePlaces
{
    public IEnumerable<ParkingLot> Handle()
    {
        return query.GetAvailablePlaces();
    }
}