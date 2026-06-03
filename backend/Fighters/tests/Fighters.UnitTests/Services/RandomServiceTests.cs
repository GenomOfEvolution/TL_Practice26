namespace Fighters.UnitTests.Services;

public class RandomServiceTests
{
    [Fact]
    public void Next_Range_ReturnsInRange()
    {
        // Arrange
        var service = new DefaultRandomService();

        // Act & Assert
        for ( int i = 0; i < 100; i++ )
        {
            int result = service.Next( 5, 10 );
            Assert.InRange( result, 5, 9 );
        }
    }

    [Fact]
    public void NextDouble_NoArgs_ReturnsInRange()
    {
        // Arrange
        var service = new DefaultRandomService();

        // Act & Assert
        for ( int i = 0; i < 100; i++ )
        {
            double result = service.NextDouble();
            Assert.InRange( result, 0.0, 1.0 );
        }
    }
}
