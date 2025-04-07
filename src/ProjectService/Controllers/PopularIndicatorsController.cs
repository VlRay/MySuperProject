using Microsoft.AspNetCore.Mvc;
using ProjectService.Dto;
using ProjectService.Services.Interfaces;

namespace ProjectService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PopularIndicatorsController : ControllerBase
{
    private readonly IMongoService _mongo;
    private readonly IUserServiceClient _userService;

    public PopularIndicatorsController(IUserServiceClient userService, IMongoService mongo)
    {
        _userService = userService;
        _mongo = mongo;
    }

    [HttpGet("{subscriptionType}")]
    public async Task<ActionResult<PopularIndicatorsResponse>> Get(string subscriptionType)
    {
        var users = await _userService.GetUsersBySubscriptionAsync(subscriptionType);

        var userIds = users.Select(u => u.Id);
        var projects = await _mongo.GetByUserIds(userIds);

        var indicatorUsage = projects
            .SelectMany(p => p.Charts)
            .SelectMany(c => c.Indicators)
            .GroupBy(i => i.Name)
            .Select(g => new IndicatorStatDto
            {
                Name = g.Key,
                Used = g.Count()
            })
            .OrderByDescending(x => x.Used)
            .Take(3)
            .ToList();

        return Ok(new PopularIndicatorsResponse { Indicators = indicatorUsage });
    }
}