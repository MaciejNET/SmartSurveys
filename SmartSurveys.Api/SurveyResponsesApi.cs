using SmartSurveys.Core.DTO;
using SmartSurveys.Core.Services;

namespace SmartSurveys.Api;

public static class SurveyResponsesApi
{
    public static WebApplication UseSurveyResponsesApi(this WebApplication app)
    {
        app.MapGet("/survey-responses/{id}", async (ISurveyResponseService surveyResponseService, int id) =>
        {
            var surveyResponse = await surveyResponseService.GetAsync(id);
            
            return surveyResponse is null ? Results.NotFound() : Results.Ok(surveyResponse);
        }).WithName("GetSurveyResponse").WithOpenApi();
        
        app.MapGet("/survey-responses/survey/{surveyId}", async (ISurveyResponseService surveyResponseService, int surveyId) =>
        {
            var surveyResponses = await surveyResponseService.GetAllResponsesForSurveyAsync(surveyId);

            return Results.Ok(surveyResponses);
        }).WithName("GetAllSurveyResponsesForSurvey").WithOpenApi();
        
        app.MapPost("/survey-responses", async (ISurveyResponseService surveyResponseService, SurveyResponseDto surveyResponseDto) =>
        {
            await surveyResponseService.CreateAsync(surveyResponseDto);

            return Results.Created($"/survey-responses/{surveyResponseDto.Id}", surveyResponseDto);
        }).WithName("CreateSurveyResponse").WithOpenApi();
        
        return app;
    }
}