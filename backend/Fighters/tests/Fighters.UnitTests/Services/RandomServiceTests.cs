using Fighters.Services.RandomService;

namespace Fighters.UnitTests.Services;

public class RandomServiceTests
{
    [Fact]
    public void DefaultRandomService_Next_ReturnsInRange()
    {
        var service = new DefaultRandomService();
        for ( int i = 0; i < 100; i++ )
        {
            int result = service.Next( 5, 10 );
            Assert.InRange( result, 5, 9 );
        }
    }

    [Fact]
    public void DefaultRandomService_NextDouble_ReturnsInRange()
    {
        var service = new DefaultRandomService();
        for ( int i = 0; i < 100; i++ )
        {
            double result = service.NextDouble();
            Assert.InRange( result, 0.0, 1.0 );
        }
    }
}
