using SmartSurveys.Core.Enums;

namespace SmartSurveys.Core.DTO;

public class QuestionDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<string> Options { get; set; } = new();
    public QuestionType Type { get; set; }
}