using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SmartSurveys.Core.DAL;
using SmartSurveys.Core.Services;
using SmartSurveys.Core.Validators;

namespace SmartSurveys.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddDapper();

        var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddScoped<ISurveyService, SurveyService>();
        services.AddScoped<ISurveyResponseService, SurveyResponseService>();

        services.AddTransient<SurveyDtoValidator>();
        services.AddTransient<SurveyDetailsDtoValidator>();
        services.AddTransient<QuestionDtoValidator>();
        services.AddTransient<SurveyResponseDtoValidator>();
        services.AddTransient<QuestionResponseDtoValidator>();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        return services;
    }

    public static WebApplication UseCore(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}