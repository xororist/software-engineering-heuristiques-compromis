﻿namespace ParkingReservation.Domain;

public class ParkingLot
{
    public int Id { get; set; }
    public char Row { get; set; }
    public int Column { get; set; }

    public ParkingLot() { }

    public ParkingLot(char row, int column)
    {
        Row = row;
        Column = column;

    }

    public static bool IsValidParkingLot(char row, int column)
    {
        return ((List<char>)['A', 'B', 'C', 'D', 'E', 'F']).Contains(row) && column is <= 10 and >= 0;
    }
}