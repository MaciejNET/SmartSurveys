using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace SmartSurveys.Core.DAL;

public class SmartSurveyDbContext
{
    private readonly IConfiguration _configuration;

    public SmartSurveyDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IDbConnection CreateConnection()
    {
        var connectionString = _configuration.GetSection("postgres")["connectionString"];
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}