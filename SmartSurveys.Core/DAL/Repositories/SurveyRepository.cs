using System.Data;
using Dapper;
using SmartSurveys.Core.Entities;

namespace SmartSurveys.Core.DAL.Repositories;

internal class SurveyRepository : ISurveyRepository
{
    private readonly IDbConnection _connection;
    
    public SurveyRepository(SmartSurveyDbContext dbContext)
    {
        _connection = dbContext.CreateConnection();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        var query = "SELECT EXISTS(SELECT 1 FROM surveys WHERE id = @id)";
        
        var exists = await _connection.ExecuteScalarAsync<bool>(query, new { id });
        
        return exists;
    }

    public async Task<Survey> GetAsync(int id)
    {
        var query = "SELECT s.*, q.* FROM surveys AS s LEFT JOIN questions AS q ON s.id = q.survey_id WHERE s.id = @id";
        var lookup = new Dictionary<int, Survey>();
        
        await _connection.QueryAsync<Survey, Question, Survey>(query, (survey, question) =>
        {
            if (!lookup.TryGetValue(survey.Id, out var surveyEntry))
            {
                surveyEntry = survey;
                surveyEntry.Questions = new List<Question>();
                lookup.Add(surveyEntry.Id, surveyEntry);
            }

            surveyEntry.Questions.Add(question);
            return surveyEntry;
        }, new { id });
        
        var survey = lookup.Values.FirstOrDefault();
        
        return survey;
    }

    public async Task<IEnumerable<Survey>> GetAllAsync()
    {
        var query = "SELECT * FROM surveys";
        var surveys = await _connection.QueryAsync<Survey>(query);

        return surveys;
    }

    public async Task CreateAsync(Survey survey)
    {
        using var transaction = _connection.BeginTransaction();
        try
        {
            var surveyQuery =
                "INSERT INTO surveys (name, description) VALUES (@Name, @Description) RETURNING id";

            var surveyId = await _connection.ExecuteScalarAsync<int>(surveyQuery, new {survey.Name, survey.Description}, transaction);
            
            var questionsQuery =
                "INSERT INTO questions (survey_id, name, type, options) VALUES (@SurveyId, @Name, @Type, @Options)";

            foreach (var question in survey.Questions) 
            {
                question.SurveyId = surveyId;
                await _connection.ExecuteAsync(questionsQuery, new
                {
                    SurveyId = surveyId,
                    Name = question.Name,
                    Type = question.Type.ToString(),
                    Options = question.Options
                }, transaction);
            }

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task UpdateAsync(Survey survey)
    {
        var query = "UPDATE surveys SET name = @name, description = @description WHERE id = @id";
        await _connection.ExecuteAsync(query, survey);
    }

    public async Task DeleteAsync(int id)
    {
        var query = "DELETE FROM surveys WHERE id = @id";
        await _connection.ExecuteAsync(query, new { id });
    }
}