using FluentValidation;
using SmartSurveys.Core.DTO;

namespace SmartSurveys.Core.Validators;

internal class SurveyDetailsDtoValidator : AbstractValidator<SurveyDetailsDto>
{
    public SurveyDetailsDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(255);

        RuleFor(x => x.Questions).ForEach(x => x.SetValidator(new QuestionDtoValidator()));
    }
}