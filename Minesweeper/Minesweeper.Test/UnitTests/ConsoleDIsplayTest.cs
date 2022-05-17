using Minesweeper.Core.Service;
using Xunit;
namespace Minesweeper.Test.UnitTests
{
    public class ConsoleDisplayTest
    {
        [Theory]
        [InlineData(0,"A")]
        [InlineData(1,"B")]
        [InlineData(2,"C")]
        [InlineData(3,"D")]
        [InlineData(4,"E")]
        [InlineData(5,"F")]
        [InlineData(6,"G")]
        [InlineData(7,"H")]
        public void ConsoleDisplay_ConvertIntToAlphabetPass(int number, string expectedAlpha)
        {
            //Arrange
            var consoleDisplayService = new ConsoleDisplayService();

            //Act
            var result = consoleDisplayService.ConvertPosToAlphabet(number);
         
            //Assert
            Assert.Equal(expectedAlpha, result);
        }
    }
}
