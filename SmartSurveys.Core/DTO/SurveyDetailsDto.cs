namespace SmartSurveys.Core.DTO;

public class SurveyDetailsDto : SurveyDto
{
    public List<QuestionDto> Questions { get; set; } = new();
}