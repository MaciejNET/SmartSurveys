using MapsterMapper;
using SmartSurveys.Core.DAL.Repositories;
using SmartSurveys.Core.DTO;
using SmartSurveys.Core.Entities;
using SmartSurveys.Core.Validators;
using SmartSurveys.Core.Results;

namespace SmartSurveys.Core.Services;

internal class SurveyService : ISurveyService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly IMapper _mapper;
    private readonly SurveyDtoValidator _surveyDtoValidator;
    private readonly SurveyDetailsDtoValidator _surveyDetailsDtoValidator;

    public SurveyService(ISurveyRepository surveyRepository, IMapper mapper, SurveyDtoValidator surveyDtoValidator, SurveyDetailsDtoValidator surveyDetailsDtoValidator)
    {
        _surveyRepository = surveyRepository;
        _mapper = mapper;
        _surveyDtoValidator = surveyDtoValidator;
        _surveyDetailsDtoValidator = surveyDetailsDtoValidator;
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

    public async Task<Result> CreateAsync(SurveyDetailsDto surveyDto)
    {
        var validationResult = await _surveyDetailsDtoValidator.ValidateAsync(surveyDto);

        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        
        var survey = _mapper.Map<Survey>(surveyDto);
        
        await _surveyRepository.CreateAsync(survey);
        
        return Result.Success();
    }

    public async Task<Result> UpdateAsync(SurveyDto surveyDto)
    {
        var exists = await _surveyRepository.ExistsAsync(surveyDto.Id);

        if (!exists)
        {
            return Result.Failure("Survey does not exists.");
        }
        
        var validationResult = await _surveyDtoValidator.ValidateAsync(surveyDto);

        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }
        
        var survey = _mapper.Map<Survey>(surveyDto);

        await _surveyRepository.UpdateAsync(survey);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id)
    {
        var exists = await _surveyRepository.ExistsAsync(id);
        
        if (!exists)
        {
            return Result.Failure("Survey does not exists.");
        }
        
        await _surveyRepository.DeleteAsync(id);
        return Result.Success();
    }
}