// --------------------------------------------------------------------------------
// <copyright filename="DbContext_Test.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.Dto.Genercis;
using System;
using System.Linq.Expressions;

namespace EngUtil.Dto
{
    public interface IMapper
    {
        DtoMap<TSource, TTarget> CreateMapping<TSource, TTarget>(string description = default)
            where TSource : class
            where TTarget : class;

        Expression<Func<TSource, TTarget>> GetExpressionMap<TSource, TTarget>()
            where TSource : class
            where TTarget : class;

        Expression GetExpressionMap(Type sourceType, Type targetType);

        void GetMapDefinition(object instance);
    }
}
