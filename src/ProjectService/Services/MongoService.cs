using MongoDB.Driver;
using ProjectService.Models;
using Microsoft.Extensions.Options;
using ProjectService.Services.Interfaces;

namespace ProjectService.Services;

public class MongoService : IMongoService
{
    private readonly IMongoCollection<Project> _projects;

    public MongoService(IOptions<MongoSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.Database);
        _projects = database.GetCollection<Project>("projects");
    }
    public MongoService(MongoSettings settings, IMongoDatabase database)
    {
        _projects = database.GetCollection<Project>("projects");
    }

    public async Task<List<Project>> GetAll() => await _projects.Find(_ => true).ToListAsync();

    public async Task<List<Project>> GetByUserIds(IEnumerable<int> userIds)
    {
        Console.WriteLine("Поиск проектов для userIds: " + string.Join(", ", userIds));

        var allProjects = await _projects.Find(FilterDefinition<Project>.Empty).ToListAsync();
        Console.WriteLine($"Всего проектов в базе: {allProjects.Count}");
        foreach (var project in allProjects)
        {
            Console.WriteLine($"Mongo >> UserId: {project.UserId}, Name: {project.Name}");
        }

        var result = await _projects.Find(p => userIds.Contains(p.UserId)).ToListAsync();

        Console.WriteLine("Найдено проектов по userIds: " + result.Count);
        return result;
    }
}
