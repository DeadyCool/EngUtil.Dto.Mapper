using System;
using System.Linq.Expressions;

namespace EngUtil.Dto
{
    public class DtoMap : IDtoMap
    {
        #region ctor

        protected DtoMap() { }

        public DtoMap(Type sourceType, Type targetType)
        {
            SourceType = sourceType;
            TargetType = targetType;
        }

        public DtoMap(Type sourceType, Type targetType, Expression expression)
            : this(sourceType, targetType)
        {
            Expression = expression;
        }

        #endregion

        #region properties

        public string Description { get; set; }

        #endregion

        #region properties: internal

        public Type SourceType { get; internal set; }

        public Type TargetType { get; internal set; }

        public Expression Expression { get; internal set; }

        #endregion

        #region methods

        /// <summary>
        /// Map a object to &lt;TTarget&gt;
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>&lt;TTarget&gt;</returns>
        public object MapObject(object instance)
        {
            if (instance.GetType() != SourceType)
                throw new ArgumentNullException($"Type not found!\r\nInstanceType is {instance.GetType().Name}");
            return ((LambdaExpression)Expression).Compile().DynamicInvoke(instance);          
        }

        #endregion
    }
}
