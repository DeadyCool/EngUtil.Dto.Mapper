using System;
using System.Linq.Expressions;

namespace engUtil.Dto
{
    public interface IMap
    {
        Type SourceType { get; }
        Type TargetType { get; }
        string Description { get; set; }
        Expression ExpressionMap { get; }
        object MapObject(object instance);
        void AddMap(Expression mapExpression);
        Expression GetExpression(); 
    }
}
