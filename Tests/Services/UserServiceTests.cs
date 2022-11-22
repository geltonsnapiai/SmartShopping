using FakeItEasy;
using MockQueryable.FakeItEasy;
using SmartShopping.Dtos;
using SmartShopping.Models;
using SmartShopping.Repositories;
using SmartShopping.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SmartShopping.Tests.Services
{
    public class UserServiceTests
    {
        private IRepository fakeRepository;
        private ITokenService fakeTokenService;
        private IValidationService fakeValidationService;

        public UserServiceTests()
        {
            this.fakeRepository = A.Fake<IRepository>();
            this.fakeTokenService = A.Fake<ITokenService>();
            this.fakeValidationService = A.Fake<IValidationService>();
        }

        private UserService CreateService()
        {
            return new UserService(
                this.fakeRepository,
                this.fakeTokenService,
                this.fakeValidationService);
        }

        [Fact]
        public async Task DeleteUserByIdAsync()
        {
            // Arrange
            var deleteId = Guid.NewGuid();
            var data = new List<User>()
            {
                new User { Id = deleteId },
                new User { Id = Guid.NewGuid() },
            };

            var service = CreateService();
            A.CallTo(() => fakeRepository.ReadAsync<User>(A<Guid>._))
                .ReturnsLazily(call => Task.FromResult(data.Find(e => e.Id == (Guid)call.Arguments[0])));

            A.CallTo(() => fakeRepository.DeleteAsync<User>(A<User>._))
                .ReturnsLazily(call =>
                {
                    data.Remove((User)call.Arguments[0]);
                    return Task.CompletedTask;
                });

            // Act
            await service.DeleteUserByIdAsync(deleteId);

            // Assert
            Assert.True(data.Count == 1);
            Assert.False(data.First().Id == deleteId);
        }

        [Fact]
        public async Task GetUserByIdAsync()
        {
            // Arrange
            var getId = Guid.NewGuid();
            var name = "Name";
            var email = "email@email.com";
            var data = new List<User>()
            {
                new User { Id = getId, Name=name, Email=email},
                new User { Id = Guid.NewGuid() },
            };

            var service = CreateService();
            A.CallTo(() => fakeRepository.ReadAsync<User>(A<Guid>._))
                .ReturnsLazily(call => Task.FromResult(data.Find(e => e.Id == (Guid)call.Arguments[0])));

            // Act
            var result = await service.GetUserByIdAsync(getId);

            // Assert
            Assert.Equal(result.Id, getId);
            Assert.Equal(result.Name, name);
            Assert.Equal(result.Email, email);
        }

        [Theory]
        [InlineData("wrong@a.com", "aA1.", "Email or password is incorrect")]
        [InlineData("a@a.com", "wrongA1.", "Email or password is incorrect")]
        public async Task LoginUserAsync(string email, string password, string exceptionMessage)
        {
            // Arrange
            LoginDto loginData = new LoginDto { Email = email, Password = password };
            var data = new List<User>()
            {
                new User() { Id = Guid.NewGuid(), Email = "a@a.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("aA1.") },
            };
            
            
            var mock = data.AsQueryable().BuildMockDbSet();
            var service = CreateService();

            A.CallTo(() => fakeRepository.Set<User>())
                .Returns(mock);

            // Act
            var ex = await Assert.ThrowsAsync<AuthenticationException>(() => service.LoginUserAsync(loginData));

            // Assert
            Assert.Equal(ex.Message, exceptionMessage);
        }

        [Theory]
        [InlineData("C087D6F8-D441-43A3-A7D9-8C55B89F8989", "", "There is no such user.")]
        [InlineData("741F3CF1-8CCA-4831-B52B-E689A108F6CE", "REFRESH", "Login expired")]
        public async Task RefreshTokensAsync(string accessToken, string refreshToken, string expetedException)
        {
            // Arrange
            TokenDto tokens = new TokenDto { AccessToken = accessToken, RefreshToken = refreshToken };
            var data = new List<User>()
            {
                new User() { Id = Guid.Parse("741F3CF1-8CCA-4831-B52B-E689A108F6CE"), RefreshToken="REFRESH", RefreshTokenExpiryTime=DateTime.MinValue },
            };

            var service = CreateService();
            var mock = data.AsQueryable().BuildMockDbSet();
            
            A.CallTo(() => fakeTokenService.GetIdFromAccessToken(A<string>._))
                .ReturnsLazily(call => Guid.Parse((string)call.Arguments[0]));

            A.CallTo(() => fakeRepository.ReadAsync<User>(A<Guid>._))
                .ReturnsLazily(call => Task.FromResult(data.Find(u => u.Id == (Guid)call.Arguments[0])));

            // Act
            var ex = await Assert.ThrowsAsync<AuthenticationException>(() => service.RefreshTokensAsync(tokens));

            // Assert
            Assert.Equal(ex.Message, expetedException);
        }

        //[Fact]
        //public async Task RegisterUserAsync_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var service = this.CreateService();
        //    RegisterDto registerData = default(global::SmartShopping.Dtos.RegisterDto);

        //    // Act
        //    var result = await service.RegisterUserAsync(
        //        registerData);

        //    // Assert
        //    Assert.True(false);
        //}

        [Fact]
        public async Task RevokeTokensByUserIdAsync()
        {
            // Arrange
            var service = this.CreateService();
            var id = Guid.Parse("7A1F3CF1-8CCA-4831-B52B-E689A108F6CE");
            var data = new List<User>()
            {
                new User() { Id = id, RefreshToken="REFRESH" },
            };
            A.CallTo(() => fakeRepository.ReadAsync<User>(A<Guid>._))
                .ReturnsLazily(call =>
                {
                    var result = data.FirstOrDefault(data => data.Id == (Guid)call.Arguments[0]);
                    return Task.FromResult(result);
                });

            // Act
            await service.RevokeTokensByUserIdAsync(id);

            // Assert
            Assert.Equal(data.Single().RefreshToken, null);
        }
    }
}
