using ParkingReservation.Domain.User;

namespace ParkingReservation.Domain;

public class Reservation
{
    public Guid Id { get; init; }
    public User.User User { get; set; }
    public ParkingLot? ParkingLot { get; set; }
    public DateTime BeginningOfReservation { get; set; }
    public DateTime EndOfReservation { get; set; }
    public bool HasBeenConfirmed { get; set; }
    public bool HasBeenCancelled { get; set; }

    public Reservation(User.User user, ParkingLot parkingLot, DateTime beginning, DateTime end)
    {
        if (!CheckReservationValidity(beginning, end))
        {
            throw new ArgumentException("Reservation dates are not valid.");
        }
        
        Id = new Guid();
        User = user;
        ParkingLot = parkingLot;
        BeginningOfReservation = beginning;
        EndOfReservation = end;
        HasBeenCancelled = false;
    }
    
    
    private bool CheckReservationValidity(DateTime beginningOfReservation, DateTime endOfReservation)
    {
        if (beginningOfReservation >= endOfReservation || endOfReservation < DateTime.Now)
            return false;
        var totalDays = (endOfReservation - beginningOfReservation).TotalDays;
        if (User.Role.Equals(Role.Manager))
        {
            if (totalDays > 30)
            {
                return false;
            };
            
        }
        else
        {
            if (totalDays > 5)
            {
                return false;
            } 
        }
        return true;
    }
    
    
    
}
