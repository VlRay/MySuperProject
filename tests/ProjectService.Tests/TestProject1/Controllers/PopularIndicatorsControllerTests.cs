using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectService.Controllers;
using ProjectService.Dto;
using ProjectService.Models;
using ProjectService.Services.Interfaces;

namespace ProjectService.Tests.Controllers
{
    public class PopularIndicatorsControllerTests
    {
        [Fact]
        public async Task Get_ReturnsCorrectIndicators()
        {
            var mockUserService = new Mock<IUserServiceClient>();
            mockUserService.Setup(x => x.GetUsersBySubscriptionAsync("Super"))
                .ReturnsAsync(new List<UserDto>
                {
                    new() { Id = 1, Name = "Test", SubscriptionType = "Super" }
                });

            var mockMongoService = new Mock<IMongoService>();
            mockMongoService.Setup(x => x.GetByUserIds(It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(new List<Project>
                {
                    new()
                    {
                        UserId = 1,
                        Charts = new List<Chart>
                        {
                            new()
                            {
                                Indicators = new List<Indicator>
                                {
                                    new() { Name = "MA", Parameters = "a=1" },
                                    new() { Name = "RSI", Parameters = "b=2" }
                                }
                            }
                        }
                    }
                });

            var controller = new PopularIndicatorsController(mockUserService.Object, mockMongoService.Object);

            var result = await controller.Get("Super");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var response = Assert.IsType<PopularIndicatorsResponse>(okResult.Value);

            Assert.Equal(2, response.Indicators.Count);
            Assert.Contains(response.Indicators, i => i.Name == "MA" && i.Used == 1);
            Assert.Contains(response.Indicators, i => i.Name == "RSI" && i.Used == 1);
        }
    }
}
