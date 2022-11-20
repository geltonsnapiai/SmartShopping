using SmartShopping.Models;

namespace SmartShopping.Tests.Models
{
    public class HelpersTests
    {
        [Theory]
        [InlineData("coca cola", "Coca cola", "coca cola")]
        [InlineData("COCA COLA", "Coca cola", "coca cola")]
        [InlineData("CoCA  cOla", "Coca cola", "coca cola")]
        [InlineData("ĄČĘĖĮ ŠŲŪŽ", "Ąčęėį šųūž", "aceei suuz")]
        public void ProcessNameTest(string name, string expectedDisplayName, string expectedSimplifiedName)
        {
            var (DisplayName, SimplifiedName) = Helpers.ProcessName(name);

            Assert.Equal(expectedDisplayName, DisplayName);
            Assert.Equal(expectedSimplifiedName, SimplifiedName);
        }
    }
}
