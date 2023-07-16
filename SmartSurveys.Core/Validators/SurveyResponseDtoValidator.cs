using FluentValidation;
using SmartSurveys.Core.DTO;

namespace SmartSurveys.Core.Validators;

internal class SurveyResponseDtoValidator : AbstractValidator<SurveyResponseDto>
{
    public SurveyResponseDtoValidator()
    {
        RuleFor(x => x.SurveyId)
            .NotEmpty();

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MinimumLength(3)
            .MinimumLength(50);

        RuleFor(x => x.QuestionResponses).ForEach(x => x.SetValidator(new QuestionResponseDtoValidator()));
    }
}