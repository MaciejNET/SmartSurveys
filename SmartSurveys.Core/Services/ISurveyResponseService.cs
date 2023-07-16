using SmartSurveys.Core.DTO;
using SmartSurveys.Core.Results;

namespace SmartSurveys.Core.Services;

public interface ISurveyResponseService
{
    Task<SurveyResponseDto> GetAsync(int id);
    Task<IEnumerable<SurveyResponseDto>> GetAllResponsesForSurveyAsync(int surveyId);
    Task<Result> CreateAsync(SurveyResponseDto surveyResponseDto);
}
