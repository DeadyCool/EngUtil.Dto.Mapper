using System;
using System.Linq.Expressions;

namespace EngUtil.Dto.Genercis
{
    public class MappingConfig<TSource, TTarget> : IMappingConfig<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        private IDtoMap<TSource, TTarget>  _dtoMap;

        public MappingConfig(IDtoMap dtoMap)
        {
            _dtoMap = (IDtoMap<TSource, TTarget>)dtoMap;
        }

        public MappingConfig(IDtoMap<TSource, TTarget> dtoMap)
        {
            _dtoMap = dtoMap;
        }

        public IDtoMap<TSource, TTarget> AddMap(Expression<Func<TSource, TTarget>> expressionMap)
        {
            ((DtoMap<TSource, TTarget>)_dtoMap).Expression = expressionMap;
            return _dtoMap;
        }
    }
}
