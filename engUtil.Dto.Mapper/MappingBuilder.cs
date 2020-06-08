// --------------------------------------------------------------------------------
// <copyright filename="MappingConfig.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.Dto.Genercis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace EngUtil.Dto
{
    public class MappingBuilder : IMappingBuilder
    {
        #region fields

        private static IList<IDtoMap> _mappingList;

        #endregion

        #region ctor

        public MappingBuilder(IList<IDtoMap> mappings)
        {
            _mappingList = mappings;
        }

        public MappingBuilder(IMapper mapper)
            : this(((Mapper)mapper)?.Mappings)
        {        
        }

        #endregion

        #region methods

        /// <summary>
        /// Create new mapping 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <returns></returns>
        public IMappingConfig<TSource, TTarget> CreateMappingFor<TSource, TTarget>(string description = default)
            where TSource : class
            where TTarget : class
        {
            var dto = new DtoMap<TSource, TTarget>
            {
                Description = description
            };            
            _mappingList.Add(dto);
            var mappingBuilder = new MappingConfig<TSource, TTarget>(dto);
            return mappingBuilder;
        }

        /// <summary>
        /// Build the Memberexpression
        /// </summary>
        /// <typeparam name="TSource">The Source Type</typeparam>
        /// <typeparam name="TTarget">The Target Type</typeparam>
        public IMappingBuilder AddMap<TSource, TTarget>(Expression<Func<TSource, TTarget>> expressionMap)
            where TSource : class
            where TTarget : class
        {
            var dtoMap = _mappingList.FirstOrDefault(x => x.SourceType == typeof(TSource) && x.TargetType == typeof(TTarget));
            if (dtoMap == null)
                throw new Exception("Dto-Map not found. First use CreateMappingFor to define the map.");
            if (dtoMap.Expression != null)
                throw new ArgumentException("Expression already set!");
            ((DtoMap<TSource, TTarget>)dtoMap).Expression = expressionMap;
            return this;
        }

        /// <summary>
        /// Map instance to &lt;TTarget&gt; Type
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public TTarget MapTo<TTarget>(object instance)
             where TTarget : class
        {
            return MappingHelper.MapTo<TTarget>(_mappingList, instance);
        }

        /// <summary>
        /// Map a Enumarable list of objects to IEnumerable&lt;TTarget&gt; Type
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="objectList">List of object</param>
        /// <returns></returns>
        public IEnumerable<TTarget> MapTo<TTarget>(IEnumerable objectList)
            where TTarget : class
        {
            return MappingHelper.MapTo<TTarget>(_mappingList, objectList);
        }

        #endregion
    }
}
