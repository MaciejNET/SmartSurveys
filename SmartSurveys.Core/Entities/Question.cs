using SmartSurveys.Core.Enums;

namespace SmartSurveys.Core.Entities;

internal class Question
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public string Name { get; set; }
    public List<string> Options { get; set; } = new();
    public QuestionType Type { get; set; }
}