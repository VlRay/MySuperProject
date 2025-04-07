using ProjectService.Dto;

namespace ProjectService.Services.Interfaces;

public interface IUserServiceClient
{
    Task<List<UserDto>> GetUsersBySubscriptionAsync(string subscriptionType);
}
