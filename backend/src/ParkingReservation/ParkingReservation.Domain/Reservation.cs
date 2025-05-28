namespace ParkingReservation.Domain;

public class Reservation
{
    public Guid Id { get; init; }
    public Guid EmployeeId { get; set; }
    public ParkingLot? ParkingLot { get; set; }
    public DateTime BeginningOfReservation { get; set; }
    public DateTime EndOfReservation { get; set; }
    public bool HasBeenConfirmed { get; set; }

    public Reservation(Guid employeeId, ParkingLot parkingLot, DateTime beginning, DateTime end)
    {
        Id = new Guid();
        EmployeeId = employeeId;
        ParkingLot = parkingLot;
        BeginningOfReservation = beginning;
        EndOfReservation = end;
    }
}
