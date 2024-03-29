using SmartSurveys.Core.DTO;
using SmartSurveys.Core.Services;

namespace SmartSurveys.Api;

public static class SurveysApi
{
    public static WebApplication UseSurveysApi(this WebApplication app)
    {
        app.MapGet("/surveys", async (ISurveyService surveyService) =>
        {
            var surveys = await surveyService.GetAllAsync();

            return Results.Ok(surveys);
        }).WithName("GetAllSurveys").WithOpenApi();
        
        app.MapGet("/surveys/{id}", async (ISurveyService surveyService, int id) =>
        {
            var survey = await surveyService.GetAsync(id);

            return survey is null ? Results.NotFound() : Results.Ok(survey);
        }).WithName("GetSurvey").WithOpenApi();
        
        app.MapPost("/surveys", async (ISurveyService surveyService, SurveyDetailsDto surveyDto) =>
        {
            var result = await surveyService.CreateAsync(surveyDto);
            
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Errors);
            }

            return Results.Created($"/surveys/{surveyDto.Id}", surveyDto);
        }).WithName("CreateSurvey").WithOpenApi();
        
        app.MapPut("/surveys/{id}", async (ISurveyService surveyService, SurveyDto surveyDto, int id) =>
        {
            surveyDto.Id = id;
            
            var result = await surveyService.UpdateAsync(surveyDto);
            
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Errors);
            }

            return Results.Ok();
        }).WithName("UpdateSurvey").WithOpenApi();
        
        app.MapDelete("/surveys/{id}", async (ISurveyService surveyService, int id) =>
        {
            var result = await surveyService.DeleteAsync(id);
            
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Errors);
            }

            return Results.Ok();
        }).WithName("DeleteSurvey").WithOpenApi();
        
        return app;
    }
}