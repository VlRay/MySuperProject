using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mongo2Go;
using MongoDB.Driver;
using ProjectService.Models;
using ProjectService.Services;
using ProjectService.Services.Interfaces;

namespace ProjectService.IntegrationTests;

public class ProjectServiceFactory : WebApplicationFactory<Program>
{
    public MongoDbRunner MongoRunner { get; } = MongoDbRunner.Start();
    public IMongoClient MongoClient => new MongoClient(MongoRunner.ConnectionString);

    public MockUserService UserServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IMongoService>();
            services.RemoveAll<IUserServiceClient>();

            services.AddSingleton<IUserServiceClient>(UserServiceMock);

            services.AddSingleton<IMongoService>(_ =>
            {
                var db = MongoClient.GetDatabase("test-db");
                return new MongoService(new MongoSettings
                {
                    ConnectionString = MongoRunner.ConnectionString,
                    Database = "test-db"
                }, db);
            });
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        MongoRunner.Dispose();
    }
}
