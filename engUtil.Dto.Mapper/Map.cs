using System;
using System.Linq.Expressions;

namespace engUtil.Dto
{
    public class Map<TSource, TTarget> : IMap
    {

        #region fields

        private IMapper _mapper;

        #endregion

        #region ctor

        public Map(IMapper mapper)
        {
            _mapper = mapper;
            SourceType = typeof(TSource);
            TargetType = typeof(TTarget);
        }

        #endregion

        #region properties

        public Type SourceType { get; private set; }

        public Type TargetType { get; private set; }

        public string Description { get; set ; }

        public Expression ExpressionMap { get; private set; }

        #endregion

        #region methods

        /// <summary>
        /// Adds a description to the map
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public Map<TSource,TTarget> AddDescription(string description)
        {
            Description = description;
            return this;
        }

        /// <summary>
        /// Build the Memberexpression
        /// </summary>
        /// <typeparam name="TSource">The Source Type</typeparam>
        /// <typeparam name="TTarget">The Target Type</typeparam>
        public void AddMap(Expression<Func<TSource, TTarget>> expressionMap)
        {
            if (ExpressionMap != null)
                throw new ArgumentException("Expression already set!");
            ExpressionMap = expressionMap;
            ((Mapper)_mapper).AddMapping(this);        
        }

        /// <summary>
        /// Build the Memberexpression using Expression&lt;Func&lt;TSource, TTarget&gt;&gt;
        /// </summary>
        /// <param name="expressionMap">Expression&lt;Func&lt;TSource, TTarget&gt;&gt;</param>
        /// <returns></returns>
        public void AddMap(Expression expressionMap)
        {
            var funcTypes = Helper.GetExpressionInputOutputTypes(expressionMap);
            if (SourceType != funcTypes.InType && TargetType != funcTypes.OutType)
                throw new ArgumentException($"Expression disaccorded with TTarget or TSource");
            if (ExpressionMap != null)
                throw new ArgumentException("Expression already set!");
            ExpressionMap = expressionMap;
            ((Mapper)_mapper).AddMapping(this);         
        }

        /// <summary>
        /// Map a object to &lt;TTarget&gt;
        /// </summary>
        /// <param name="instance"></param>
        /// <returns>&lt;TTarget&gt;</returns>
        public object MapObject(object instance)
        {
            if (instance.GetType() == SourceType)
                return ((Expression<Func<TSource, TTarget>>)ExpressionMap).Compile().Invoke((TSource)instance);
            throw new ArgumentNullException($"Type not found!\r\nInstanceType is {instance.GetType().Name}");
        }

        public Expression GetExpression()
        {
            return ExpressionMap;
        }

        #endregion
    }
}
