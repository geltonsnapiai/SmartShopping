using SmartShopping.Models;

namespace SmartShopping.Tests.Models
{
    public class ProductTests
    {
        [Theory]
        [InlineData("coca cola", "Coca cola", "coca cola")]
        [InlineData("COCA COLA", "Coca cola", "coca cola")]
        [InlineData("CoCA  cOla", "Coca cola", "coca cola")]
        [InlineData("ĄČĘĖĮ ŠŲŪŽ", "Ąčęėį šųūž", "aceei suuz")]
        public void ProductSetNameTest(string name, string expectedDisplayName, string expectedSimplifiedName)
        {
            var product = new Product();

            product.SetName(name);

            Assert.Equal(expectedDisplayName, product.DisplayName);
            Assert.Equal(expectedSimplifiedName, product.SimplifiedName);
        }
    }
}
