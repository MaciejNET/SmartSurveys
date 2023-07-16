using System.Data;
using Dapper;
using SmartSurveys.Core.Entities;

namespace SmartSurveys.Core.DAL.Repositories;

internal class SurveyResponseRepository : ISurveyResponseRepository
{
    private readonly IDbConnection _connection;

    public SurveyResponseRepository(SmartSurveyDbContext dbContext)
    {
        _connection = dbContext.CreateConnection();
    }
    
    public async Task<SurveyResponse> GetAsync(int id)
    {
        var query = 
            "SELECT s.*, q.* FROM survey_responses AS s LEFT JOIN question_responses AS q ON s.id = q.survey_response_id WHERE s.id = @id";

        var surveyResponseDictionary = new Dictionary<int, SurveyResponse>();

        await _connection.QueryAsync<SurveyResponse, QuestionResponse, SurveyResponse>(
            query,
            (surveyResponse, questionResponse) =>
            {
                if (!surveyResponseDictionary.TryGetValue(surveyResponse.Id, out var currentSurveyResponse))
                {
                    currentSurveyResponse = surveyResponse;
                    currentSurveyResponse.QuestionResponses = new List<QuestionResponse>();
                    surveyResponseDictionary.Add(currentSurveyResponse.Id, currentSurveyResponse);
                }

                if (questionResponse != null)
                {
                    currentSurveyResponse.QuestionResponses.Add(questionResponse);
                }

                return surveyResponse;
            },
            new { id }
        );

        var surveyResponse = surveyResponseDictionary.Values.FirstOrDefault();

        return surveyResponse;
    }



    public async Task<IEnumerable<SurveyResponse>> GetAllResponsesForSurveyAsync(int surveyId)
    {
        var query =
            "SELECT s.*, q.* FROM survey_responses AS s LEFT JOIN question_responses AS q ON s.id = q.survey_response_id WHERE s.survey_id = @surveyId";
        
        var lookup = new Dictionary<int, SurveyResponse>();
        
        await _connection.QueryAsync<SurveyResponse, QuestionResponse, SurveyResponse>(query, (surveyResponse, questionResponse) =>
        {
            if (!lookup.TryGetValue(surveyResponse.Id, out var surveyResponseEntry))
            {
                surveyResponseEntry = surveyResponse;
                surveyResponseEntry.QuestionResponses = new List<QuestionResponse>();
                lookup.Add(surveyResponseEntry.Id, surveyResponseEntry);
            }

            surveyResponseEntry.QuestionResponses.Add(questionResponse);
            return surveyResponseEntry;
        }, new { surveyId });
        
        var surveyResponses = lookup.Values.ToList();
        
        return surveyResponses;
    }

    public async Task CreateAsync(SurveyResponse surveyResponse)
    {
        using var transaction = _connection.BeginTransaction();
        try
        {  
            var query = "INSERT INTO survey_responses (survey_id, full_name) VALUES (@SurveyId, @FullName) RETURNING id";
            var surveyResponseId = await _connection.ExecuteScalarAsync<int>(query, new {surveyResponse.SurveyId, surveyResponse.FullName}, transaction);
            
            if (surveyResponseId > 0)
            {
                var questionsQuery =
                    "INSERT INTO question_responses (survey_response_id, question_id, answer) VALUES (@SurveyResponseId, @QuestionId, @Answer)";
                
                foreach (var questionResponse in surveyResponse.QuestionResponses)
                {
                    questionResponse.SurveyResponseId = surveyResponseId;
                    await _connection.ExecuteAsync(questionsQuery, new
                    {
                        SurveyResponseId = surveyResponseId,
                        QuestionId = questionResponse.QuestionId,
                        Answer = questionResponse.Answer
                    }, transaction);
                }
                
                transaction.Commit();
            }
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
}