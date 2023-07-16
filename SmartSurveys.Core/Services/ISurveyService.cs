using SmartSurveys.Core.DTO;

namespace SmartSurveys.Core.Services;

public interface ISurveyService
{
    Task<SurveyDetailsDto> GetAsync(int id);
    Task<IEnumerable<SurveyDto>> GetAllAsync();
    Task CreateAsync(SurveyDetailsDto surveyDto);
    Task UpdateAsync(SurveyDto surveyDto);
    Task DeleteAsync(int id);
}