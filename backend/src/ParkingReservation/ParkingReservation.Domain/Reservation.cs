using ParkingReservation.Domain.User;

namespace ParkingReservation.Domain;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User.User User { get; set; } = default!;
    public int? ParkingLotId { get; set; }
    public ParkingLot? ParkingLot { get; set; }
    public DateTime BeginningOfReservation { get; set; }
    public DateTime EndOfReservation { get; set; }
    public bool HasBeenConfirmed { get; set; }
    public bool HasBeenCancelled { get; set; }

    public Reservation() {}

    public Reservation(User.User user, ParkingLot parkingLot, DateTime beginning, DateTime end)
    {
        if (!CheckReservationValidity(beginning, end))
            throw new ArgumentException("Reservation dates are not valid.");

        Id = Guid.NewGuid();
        User = user;
        BeginningOfReservation = beginning;
        EndOfReservation = end;
        HasBeenCancelled = false;
        ParkingLot = parkingLot;
        HasBeenConfirmed = false;
    }

    private bool CheckReservationValidity(DateTime beginningOfReservation, DateTime endOfReservation)
    {
        if (beginningOfReservation >= endOfReservation || endOfReservation < DateTime.Now)
            return false;

        var totalDays = (endOfReservation - beginningOfReservation).TotalDays;

        return User.Role == Role.Manager ? totalDays <= 30 : totalDays <= 5;
    }
}