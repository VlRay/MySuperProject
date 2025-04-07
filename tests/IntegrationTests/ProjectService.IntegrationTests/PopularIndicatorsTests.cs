using FluentAssertions;
using ProjectService.Dto;
using ProjectService.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProjectService.IntegrationTests;

public class PopularIndicatorsTests : IClassFixture<ProjectServiceFactory>
{
    private readonly ProjectServiceFactory _factory;
    private readonly HttpClient _client;

    public PopularIndicatorsTests(ProjectServiceFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPopularIndicators_ReturnsCorrectResult()
    {
        _factory.UserServiceMock.UsersToReturn = new List<UserDto>
        {
            new() { Id = 1, Name = "John", SubscriptionType = "Super" }
        };

        var mongo = _factory.MongoClient.GetDatabase("test-db");
        var collection = mongo.GetCollection<Project>("projects");

        await collection.InsertManyAsync(new[]
        {
            new Project
            {
                UserId = 1,
                Name = "Test project",
                Charts = new List<Chart>
                {
                    new()
                    {
                        Symbol = "EURUSD",
                        Timeframe = "M5",
                        Indicators = new List<Indicator>
                        {
                            new() { Name = "MA", Parameters = "a=1" },
                            new() { Name = "RSI", Parameters = "b=2" }
                        }
                    }
                }
            }
        });

        var response = await _client.GetFromJsonAsync<PopularIndicatorsResponse>("/api/PopularIndicators/Super");

        response.Should().NotBeNull();
        response!.Indicators.Should().HaveCount(2);
        response.Indicators.Should().Contain(i => i.Name == "MA" && i.Used == 1);
        response.Indicators.Should().Contain(i => i.Name == "RSI" && i.Used == 1);
    }
}
