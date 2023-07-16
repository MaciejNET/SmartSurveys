using System.Data;
using Dapper;
using SmartSurveys.Core.Enums;

namespace SmartSurveys.Core.DAL.TypeHandlers;

internal class OptionsTypeHandler : SqlMapper.TypeHandler<List<string>>
{
    public override void SetValue(IDbDataParameter parameter, List<string> value)
    {
        parameter.Value = string.Join(';', value);
    }

    public override List<string> Parse(object value)
    {
        if (value is null or DBNull)
        {
            return new();
        }
        
        return ((string) value).Split(';').ToList();
    }
}