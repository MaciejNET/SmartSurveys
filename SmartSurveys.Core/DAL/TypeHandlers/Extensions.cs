using Dapper;
using Microsoft.Extensions.DependencyInjection;
using SmartSurveys.Core.DAL.TypeHandlers;

namespace SmartSurveys.Core.DAL.TypeHandlers;

internal static class Extensions
{
    public static void AddTypeHandlers(this IServiceCollection services)
    {
        SqlMapper.AddTypeHandler(new OptionsTypeHandler());
        SqlMapper.AddTypeHandler(new QuestionTypeTypeHandler());
    }
}