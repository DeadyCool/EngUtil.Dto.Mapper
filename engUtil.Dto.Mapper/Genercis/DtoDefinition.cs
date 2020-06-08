// --------------------------------------------------------------------------------
// <copyright filename="DtoDefinition.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Linq.Expressions;

namespace EngUtil.Dto.Genercis
{
    public abstract class DtoDefinition<TSource, TTarget> : DtoDefinition, IDtoDefinition<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        [Map]
        public abstract Expression<Func<TTarget, TSource>> ToSourceDto { get; }

        [Map]
        public abstract Expression<Func<TSource, TTarget>> ToTargetDto { get; }
    }
}
