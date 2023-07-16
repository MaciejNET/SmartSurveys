using SmartSurveys.Core.DTO;

namespace SmartSurveys.Core.Services;

public interface ISurveyResponseService
{
    Task<SurveyResponseDto> GetAsync(int id);
    Task<IEnumerable<SurveyResponseDto>> GetAllResponsesForSurveyAsync(int surveyId);
    Task CreateAsync(SurveyResponseDto surveyResponseDto);
}
