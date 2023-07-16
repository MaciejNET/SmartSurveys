using MapsterMapper;
using SmartSurveys.Core.DAL.Repositories;
using SmartSurveys.Core.DTO;
using SmartSurveys.Core.Entities;

namespace SmartSurveys.Core.Services;

internal class SurveyResponseService : ISurveyResponseService
{
    private readonly ISurveyResponseRepository _surveyResponseRepository;
    private readonly IMapper _mapper;

    public SurveyResponseService(ISurveyResponseRepository surveyResponseRepository, IMapper mapper)
    {
        _surveyResponseRepository = surveyResponseRepository;
        _mapper = mapper;
    }

    public async Task<SurveyResponseDto> GetAsync(int id)
    {
        var surveyResponse = await _surveyResponseRepository.GetAsync(id);
        
        return _mapper.Map<SurveyResponseDto>(surveyResponse);
    }

    public async Task<IEnumerable<SurveyResponseDto>> GetAllResponsesForSurveyAsync(int surveyId)
    {
        var surveyResponses = await _surveyResponseRepository.GetAllResponsesForSurveyAsync(surveyId);
        
        return _mapper.Map<IEnumerable<SurveyResponseDto>>(surveyResponses);
    }

    public async Task CreateAsync(SurveyResponseDto surveyResponseDto)
    {
        var surveyResponse = _mapper.Map<SurveyResponse>(surveyResponseDto);
        
        await _surveyResponseRepository.CreateAsync(surveyResponse);
    }
}