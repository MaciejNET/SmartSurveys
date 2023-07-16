using System.Data;
using Dapper;
using SmartSurveys.Core.Enums;

namespace SmartSurveys.Core.DAL.TypeHandlers;

internal class QuestionTypeTypeHandler : SqlMapper.TypeHandler<QuestionType>
{
    public override void SetValue(IDbDataParameter parameter, QuestionType value)
    {
        parameter.Value = parameter.ToString();
    }

    public override QuestionType Parse(object value)
    {
        return value switch
        {
            "SingleChoice" => QuestionType.SingleChoice,
            "MultipleChoice" => QuestionType.MultipleChoice,
            "Text" => QuestionType.Text,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }
}