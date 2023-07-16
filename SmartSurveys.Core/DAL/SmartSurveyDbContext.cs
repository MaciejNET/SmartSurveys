using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace SmartSurveys.Core.DAL;

internal class SmartSurveyDbContext
{
    private readonly IConfiguration _configuration;

    public SmartSurveyDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    internal IDbConnection CreateConnection()
    {
        var connectionString = _configuration.GetSection("postgres")["connectionString"];
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
    
    internal IDbConnection CreateTestConnection()
    {
        var connectionString = _configuration.GetSection("postgres")["testConnectionString"];
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}