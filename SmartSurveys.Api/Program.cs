using SmartSurveys.Api;
using SmartSurveys.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCore();

var app = builder.Build();

app.Use(async (ctx, next) =>
{
    try
    {
        await next(ctx);
    }
    catch (Exception e)
    {
        var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger<Program>();
        
        logger.LogError(e, e.Message);
        
        ctx.Response.StatusCode = 500;
        await ctx.Response.WriteAsJsonAsync("An error occurred");
    }
});

app.MapControllers();

app.UseSurveysApi();

app.UseSurveyResponsesApi();

app.UseCore();

app.Run();