using SmartSurveys.Core.Entities;

namespace SmartSurveys.Core.DAL.Repositories;

internal interface ISurveyRepository
{
    Task<Survey> GetAsync(int id);
    Task<IEnumerable<Survey>> GetAllAsync();
    Task CreateAsync(Survey survey);
    Task UpdateAsync(Survey survey);
    Task DeleteAsync(int id);
}