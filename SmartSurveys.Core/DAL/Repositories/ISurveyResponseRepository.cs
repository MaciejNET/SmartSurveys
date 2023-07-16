using SmartSurveys.Core.Entities;

namespace SmartSurveys.Core.DAL.Repositories;

internal interface ISurveyResponseRepository
{
    Task<SurveyResponse> GetAsync(int id);
    Task<IEnumerable<SurveyResponse>> GetAllResponsesForSurveyAsync(int surveyId);
    Task CreateAsync(SurveyResponse surveyResponse);
}