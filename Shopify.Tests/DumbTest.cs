
using Xunit;

namespace Shopify.Tests
{
    public class DumbTest
    {
        [Theory]
        [InlineData(5,5,10)]
        [InlineData(6,6,12)]

        public void Add_TwoNumbers_ReturnsSum(int a,int b,int sum)
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            int result = calculator.Add(a, b);

            // Assert
            Assert.Equal(sum, result);
        }
    }
}
