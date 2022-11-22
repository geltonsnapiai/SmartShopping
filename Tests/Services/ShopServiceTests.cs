using FakeItEasy;
using MockQueryable.FakeItEasy;
using SmartShopping.Models;
using SmartShopping.Repositories;
using SmartShopping.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SmartShopping.Tests.Services
{
    public class ShopServiceTests
    {
        private IRepository fakeRepository;

        public ShopServiceTests()
        {
            fakeRepository = A.Fake<IRepository>();
        }

        private ShopService CreateService()
        {
            return new ShopService(fakeRepository);
        }

        [Fact]
        public async Task AddShopAsyncTest()
        {
            // Arrange
            var service = CreateService();
            string name = "Shop";

            A.CallTo(() => fakeRepository.CreateAsync<Shop>(A<Shop>._))
                .ReturnsLazily(call => {
                    return Task.FromResult((Shop)call.Arguments[0]);
                });

            // Act
            var result = await service.AddShopAsync(name);

            // Assert
            Assert.Equal(result.Name, name);
        }

        [Fact]
        public async Task GetAllAsyncTest()
        {
            // Arrange
            var service = CreateService();
            var shops = new List<Shop>()
            {
                new Shop() { Id = Guid.NewGuid(), Name = "Shop1" },
                new Shop() { Id = Guid.NewGuid(), Name = "Shop2" },
            };
            var mock = shops.AsQueryable().BuildMockDbSet();

            A.CallTo(() => fakeRepository.Set<Shop>())
                .Returns(mock);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.True(result.Count == 2);
            Assert.Equal(result.AsEnumerable().First().Id, shops[0].Id);
            Assert.Equal(result.AsEnumerable().First().Name, shops[0].Name);
            Assert.Equal(result.AsEnumerable().Last().Id, shops[1].Id);
            Assert.Equal(result.AsEnumerable().Last().Name, shops[1].Name);
        }
    }
}
