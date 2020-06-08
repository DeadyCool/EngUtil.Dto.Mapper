using System;
using System.Linq.Expressions;

namespace EngUtil.Dto.Genercis
{
    public interface IDtoMap<TSource, TTarget> : IDtoMap
        where TSource : class
        where TTarget : class
    {
        new Expression<Func<TSource, TTarget>>  Expression { get; }

        new TTarget MapObject(object instance);
    }
}
