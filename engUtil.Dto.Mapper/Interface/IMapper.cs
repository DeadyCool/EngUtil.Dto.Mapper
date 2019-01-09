using System;
using System.Linq.Expressions;

namespace engUtil.Dto
{
    public interface IMapper
    {
        Map<TSource, TTarget> CreateMappingFor<TSource, TTarget>()
            where TSource : class
            where TTarget : class;

        Expression<Func<TSource, TTarget>> GetExpressionMap<TSource, TTarget>()
            where TSource : class
            where TTarget : class;

        Expression GetExpressionMap(Type sourceType, Type targetType);

        TTarget MapTo<TTarget>(object instance);      
    }
}
