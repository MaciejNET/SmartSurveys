using MapsterMapper;
using SmartSurveys.Core.DAL.Repositories;
using SmartSurveys.Core.DTO;
using SmartSurveys.Core.Entities;
using SmartSurveys.Core.Results;
using SmartSurveys.Core.Validators;

namespace SmartSurveys.Core.Services;

internal class SurveyResponseService : ISurveyResponseService
{
    private readonly ISurveyResponseRepository _surveyResponseRepository;
    private readonly IMapper _mapper;
    private readonly SurveyResponseDtoValidator _surveyResponseDtoValidator;
    private readonly ISurveyRepository _surveyRepository;

    public SurveyResponseService(ISurveyResponseRepository surveyResponseRepository, IMapper mapper, SurveyResponseDtoValidator surveyResponseDtoValidator, ISurveyRepository surveyRepository)
    {
        _surveyResponseRepository = surveyResponseRepository;
        _mapper = mapper;
        _surveyResponseDtoValidator = surveyResponseDtoValidator;
        _surveyRepository = surveyRepository;
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

    public async Task<Result> CreateAsync(SurveyResponseDto surveyResponseDto)
    {
        var surveyExists = await _surveyRepository.ExistsAsync(surveyResponseDto.SurveyId);
        
        if (!surveyExists)
        {
            return Result.Failure("Survey does not exist");
        }
        
        var validationResult = await _surveyResponseDtoValidator.ValidateAsync(surveyResponseDto);
        
        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        
        var surveyResponse = _mapper.Map<SurveyResponse>(surveyResponseDto);
        
        await _surveyResponseRepository.CreateAsync(surveyResponse);
        
        return Result.Success();
    }
}