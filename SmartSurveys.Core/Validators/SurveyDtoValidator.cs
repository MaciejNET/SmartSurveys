using FluentValidation;
using SmartSurveys.Core.DTO;

namespace SmartSurveys.Core.Validators;

internal class SurveyDtoValidator : AbstractValidator<SurveyDto>
{
    public SurveyDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(255);
    }
}