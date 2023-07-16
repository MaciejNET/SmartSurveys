using Microsoft.Extensions.DependencyInjection;
using SmartSurveys.Core.DAL.Repositories;
using SmartSurveys.Core.DAL.TypeHandlers;

namespace SmartSurveys.Core.DAL;

internal static class Extensions
{
    public static IServiceCollection AddDapper(this IServiceCollection services)
    {
        services.AddTypeHandlers();
        services.AddScoped<SmartSurveyDbContext>();
        services.AddHostedService<DatabaseInitializer>();
        services.AddScoped<ISurveyRepository, SurveyRepository>();
        services.AddScoped<ISurveyResponseRepository, SurveyResponseRepository>();
        
        return services;
    }
}