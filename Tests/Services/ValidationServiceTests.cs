using FakeItEasy;
using SmartShopping.Services;
using SmartShopping.Models;

namespace SmartShopping.Tests.Services
{
    public class ValidationServiceTests
    {
        // Emails taken from https://gist.github.com/cjaoude/fd9910626629b53c4d25
        [Theory]
        [InlineData("email@example.com", false, null, true)]
        [InlineData("email@example.com", true, "Email is taken", false)]
        [InlineData("firstname.lastname@example.com", false, null, true)]
        [InlineData("firstname.lastname@example.com", true, "Email is taken", false)]
        [InlineData("_______@example.com", false, null, true)]
        [InlineData("_______@example.com", true, "Email is taken", false)]
        [InlineData("plainaddress", false, "Invalid email", false)]
        [InlineData("plainaddress", true, "Invalid email", false)]
        [InlineData("", false, "Email is empty", false)]
        [InlineData("", true, "Email is empty", false)]
        [InlineData(null, false, "Email is empty", false)]
        [InlineData(null, true, "Email is empty", false)]
        [InlineData("email@example@example.com", false, "Invalid email", false)]
        [InlineData("email@example@example.com", true, "Invalid email", false)]
        [InlineData("email@111.222.333.44444", false, "Invalid email", false)]
        [InlineData("email@111.222.333.44444", true, "Invalid email", false)]
        public void EmailValidationTest(string email, bool emailIsTaken, string expectedErrorMessage, bool expectedResult)
        {
            var userService = A.Fake<IUserService>();
            A.CallTo(() => userService.GetUserByEmail(email))
                .Returns(emailIsTaken ? new User { Email = email } : null);
            var validatorService = new ValidationService(userService);

            bool result = validatorService.ValidateEmail(email, out string? errorMessage);

            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedErrorMessage, errorMessage);
        }

        [Theory]
        [InlineData("username", null, true)]
        [InlineData("", "Username is empty", false)]
        [InlineData(null, "Username is empty", false)]
        public void UsernameValidationTest(string username, string expectedErrorMessage, bool expectedResult)
        {
            var userService = A.Fake<IUserService>();
            var validatorService = new ValidationService(userService);

            bool result = validatorService.ValidateUsername(username, out string? errorMessage);

            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedErrorMessage, errorMessage);
        }

        [Theory]
        [InlineData("", "Password is empty", false)]
        [InlineData(null, "Password is empty", false)]
        [InlineData("A", "Password must contain atleast one lower case character", false)]
        [InlineData("a", "Password must contain atleast one upper case character", false)]
        [InlineData("aA", "Password must contain atleast one digit", false)]
        [InlineData("aA1", "Password must contain atleast one of the special characters (-+_!@#$%^&*.,?)", false)]
        [InlineData("aA1.", null, true)]
        public void PasswordValidationTest(string password, string expectedErrorMessage, bool expectedResult)
        {
            var userService = A.Fake<IUserService>();
            var validatorService = new ValidationService(userService);

            bool result = validatorService.ValidatePassword(password, out string? errorMessage);

            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedErrorMessage, errorMessage);
        }
    }
}