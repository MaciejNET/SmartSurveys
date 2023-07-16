namespace SmartSurveys.Core.Entities;

internal class SurveyResponse
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public string FullName { get; set; }
    public List<QuestionResponse> QuestionResponses { get; set; }
}