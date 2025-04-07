using ProjectService.Models;

namespace ProjectService.Services.Interfaces;

public interface IMongoService
{
    Task<List<Project>> GetByUserIds(IEnumerable<int> userIds);
}
