using ParkingReservation.Domain;
using ParkingReservation.Domain.Query;

namespace ParkingReservation.Application.UsesCases;

public interface IGetAvailablePlaces
{
    IEnumerable<AvailableParkingLotsInfo> Handle();
}

public class AvailableParkingLotsInfo(char row, int column, bool isAvailable);

public class GetAvailablePlaces(IQueryAvailablePlaces query) : IGetAvailablePlaces
{
    public IEnumerable<AvailableParkingLotsInfo> Handle()
    {
        return query.GetAvailablePlaces().Select(x => 
            new AvailableParkingLotsInfo(
                row:x.Row, 
                column:x.Column, 
                isAvailable:x.IsAvailable));
    }
}