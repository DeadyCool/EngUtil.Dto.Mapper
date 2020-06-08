// --------------------------------------------------------------------------------
// <copyright filename="ExpressionMap.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Linq.Expressions;

namespace EngUtil.Dto.Genercis
{
    public class DtoMap<TSource, TTarget> : DtoMap, IDtoMap<TSource, TTarget>
        where TSource : class
        where TTarget : class
    {
        #region ctor

        public DtoMap()
            : base(typeof(TSource), typeof(TTarget))
        {
        }

        public DtoMap(Expression<Func<TSource,TTarget>> expression)
            : base(typeof(TSource), typeof(TTarget), expression)
        {
        }

        #endregion

        #region properties

        Expression<Func<TSource, TTarget>> IDtoMap<TSource, TTarget>.Expression 
        { 
            get => (Expression<Func<TSource, TTarget>>)Expression;            
        }

        #endregion

        #region methods

        /// <summary>
        /// Map a object to &lt;TTarget&gt;
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        TTarget IDtoMap<TSource, TTarget>.MapObject(object instance)
        { 
            return (TTarget)MapObject(instance);
        }

        #endregion
    }
}
