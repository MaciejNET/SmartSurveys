namespace SmartSurveys.Core.DTO;

public class SurveyResponseDto
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public string FullName { get; set; }
    public List<QuestionResponseDto> QuestionResponses { get; set; } = new();
}