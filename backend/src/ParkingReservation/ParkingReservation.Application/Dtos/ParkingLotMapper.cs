using ParkingReservation.Domain;

namespace ParkingReservation.Application.Dtos;

public class ParkingLotMapper
{
    public static IEnumerable<ParkingLotDto> ParkingLotListToDtos(IEnumerable<ParkingLot> parkingLots)
    {
        IEnumerable<ParkingLotDto> result = new List<ParkingLotDto>();
        foreach (ParkingLot p in parkingLots)
        {
            result=result.Append(new ParkingLotDto(p.Row, p.Column, p.IsAvailable));
        }

        return result;
    }
}