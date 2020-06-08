// --------------------------------------------------------------------------------
// <copyright filename="DtoDefinition.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Linq.Expressions;

namespace EngUtil.Dto.Genercis
{
    public interface IDtoDefinition<TSource,TTarget> : IDtoDefinition
        where TSource : class
        where TTarget : class
    {
        Expression<Func<TTarget, TSource>> ToSourceDto { get; }
        
        Expression<Func<TSource, TTarget>> ToTargetDto { get; }
    }
}
