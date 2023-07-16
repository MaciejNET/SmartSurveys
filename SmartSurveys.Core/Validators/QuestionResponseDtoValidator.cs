using FluentValidation;
using SmartSurveys.Core.DTO;

namespace SmartSurveys.Core.Validators;

internal class QuestionResponseDtoValidator : AbstractValidator<QuestionResponseDto>
{
    public QuestionResponseDtoValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEmpty();

        RuleFor(x => x.Answer)
            .NotEmpty();
    }
}