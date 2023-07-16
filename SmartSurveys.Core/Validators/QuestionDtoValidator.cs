using FluentValidation;
using SmartSurveys.Core.DTO;

namespace SmartSurveys.Core.Validators;

internal class QuestionDtoValidator : AbstractValidator<QuestionDto>
{
    public QuestionDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MinimumLength(150);
    }
}