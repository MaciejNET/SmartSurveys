using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SmartSurveys.Core.DAL;

internal class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SmartSurveyDbContext>();
        
        if (!await DatabaseExists(dbContext))
        {
            var connection = dbContext.CreateTestConnection();
            await connection.ExecuteAsync("CREATE DATABASE smart_surveys");

            connection.ChangeDatabase("smart_surveys");

            await connection.ExecuteAsync(@"
            CREATE TABLE surveys (
                id SERIAL PRIMARY KEY,
                name VARCHAR(255),
                description VARCHAR(255)
            );

            CREATE TABLE questions (
                id SERIAL PRIMARY KEY,
                survey_id INT,
                name VARCHAR(255),
                options TEXT,
                type VARCHAR(50),
                FOREIGN KEY (survey_id) REFERENCES surveys(id) ON DELETE CASCADE
            );

            CREATE TABLE survey_responses (
                id SERIAL PRIMARY KEY,
                survey_id INT,
                full_name VARCHAR(255),
                FOREIGN KEY (survey_id) REFERENCES surveys(id) ON DELETE CASCADE
            );

            CREATE TABLE question_responses (
                id SERIAL PRIMARY KEY,
                survey_response_id INT,
                question_id INT,
                answer VARCHAR(255),
                FOREIGN KEY (survey_response_id) REFERENCES survey_responses(id) ON DELETE CASCADE,
                FOREIGN KEY (question_id) REFERENCES questions(id) ON DELETE CASCADE
            );");
        }
    }

    private async Task<bool> DatabaseExists(SmartSurveyDbContext dbContext)
    {
        var connection = dbContext.CreateTestConnection();
        var query = "SELECT 1 FROM pg_database WHERE datname = 'smart_surveys'";
        
        var exists = await connection.ExecuteScalarAsync<bool>(query);

        return exists;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}