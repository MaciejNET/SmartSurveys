using SmartSurveys.Api;
using SmartSurveys.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCore();

var app = builder.Build();


app.MapControllers();

app.UseSurveysApi();

app.UseSurveyResponsesApi();

app.UseCore();

app.Run();