using ProjectService.Dto;
using ProjectService.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectService.IntegrationTests;

public class MockUserService : IUserServiceClient
{
    public List<UserDto> UsersToReturn { get; set; } = new();

    public Task<List<UserDto>> GetUsersBySubscriptionAsync(string subscriptionType)
    {
        return Task.FromResult(UsersToReturn);
    }
}
