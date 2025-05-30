namespace ParkingReservation.Domain.Tests
{
    public class ParkingLotTests
    {
        [Theory]
        [InlineData('A', 0, true)]
        [InlineData('F', 10, true)]
        [InlineData('C', 5, true)]
        [InlineData('G', 5, false)]   
        [InlineData('B', -1, false)]  
        [InlineData('E', 11, false)]  
        [InlineData('X', 100, false)]
        public void IsValidParkingLot_ShouldReturnExpectedResult(char row, int column, bool isAvailable)
        {
            var result = ParkingLot.IsValidParkingLot(row, column);

            Assert.Equal(isAvailable, result);
        }
    }
}
