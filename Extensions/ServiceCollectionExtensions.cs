using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Interfaces;
using TodoApi.Application.Services;
using TodoApi.Infrastructure.Data;

namespace TodoApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<TodoContext>(options =>
            options.UseInMemoryDatabase("TodoList"));
        services.AddDatabaseDeveloperPageExceptionFilter();
        return services;
    }

    public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        services.AddOpenApiDocument(config =>
        {
            config.DocumentName = "TodoAPI";
            config.Title = "TodoAPI v1";
            config.Version = "v1";
        });
        return services;
    }
}
