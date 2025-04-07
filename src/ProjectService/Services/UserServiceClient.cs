using Flurl.Http;
using Microsoft.Extensions.Configuration;
using ProjectService.Dto;
using ProjectService.Services.Interfaces;

namespace ProjectService.Services;

public class UserServiceClient : IUserServiceClient
{
    private readonly IConfiguration _config;

    public UserServiceClient(IConfiguration config)
    {
        _config = config;
    }

    public async Task<List<UserDto>> GetUsersBySubscriptionAsync(string subscriptionType)
    {
        var userServiceUrl = _config["UserService:BaseUrl"];
        var url = $"{userServiceUrl}/users/bySubscription/{subscriptionType}";
        var users = await url.GetJsonAsync<List<UserDto>>();
        return users;
    }
}
