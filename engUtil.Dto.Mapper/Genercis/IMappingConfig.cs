using System;
using System.Linq.Expressions;

namespace EngUtil.Dto.Genercis
{
    public interface IMappingConfig<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        IDtoMap<TSource, TTarget> AddMap(Expression<Func<TSource, TTarget>> expressionMap);
    }
}
