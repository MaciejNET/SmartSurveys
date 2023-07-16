namespace SmartSurveys.Core.Entities;

internal class QuestionResponse
{
    public int Id { get; set; }
    public int SurveyResponseId { get; set; }
    public int QuestionId { get; set; }
    public string Answer { get; set; }
}