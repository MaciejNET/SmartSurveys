using SmartSurveys.Core.DTO;
using SmartSurveys.Core.Results;

namespace SmartSurveys.Core.Services;

public interface ISurveyService
{
    Task<SurveyDetailsDto> GetAsync(int id);
    Task<IEnumerable<SurveyDto>> GetAllAsync();
    Task<Result> CreateAsync(SurveyDetailsDto surveyDto);
    Task<Result> UpdateAsync(SurveyDto surveyDto);
    Task<Result> DeleteAsync(int id);
}