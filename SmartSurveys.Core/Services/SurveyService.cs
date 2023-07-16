using MapsterMapper;
using SmartSurveys.Core.DAL.Repositories;
using SmartSurveys.Core.DTO;
using SmartSurveys.Core.Entities;

namespace SmartSurveys.Core.Services;

internal class SurveyService : ISurveyService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly IMapper _mapper;

    public SurveyService(ISurveyRepository surveyRepository, IMapper mapper)
    {
        _surveyRepository = surveyRepository;
        _mapper = mapper;
    }

    public async Task<SurveyDetailsDto> GetAsync(int id)
    {
        var survey = await _surveyRepository.GetAsync(id);
        
        return _mapper.Map<SurveyDetailsDto>(survey);
    }

    public async Task<IEnumerable<SurveyDto>> GetAllAsync()
    {
        var surveys = await _surveyRepository.GetAllAsync();
        
        return _mapper.Map<IEnumerable<SurveyDto>>(surveys);
    }

    public async Task CreateAsync(SurveyDetailsDto surveyDto)
    {
        var survey = _mapper.Map<Survey>(surveyDto);
        
        await _surveyRepository.CreateAsync(survey);
    }

    public async Task UpdateAsync(SurveyDto surveyDto)
    {
        var survey = _mapper.Map<Survey>(surveyDto);

        await _surveyRepository.UpdateAsync(survey);
    }

    public async Task DeleteAsync(int id)
    {
        await _surveyRepository.DeleteAsync(id);
    }
}