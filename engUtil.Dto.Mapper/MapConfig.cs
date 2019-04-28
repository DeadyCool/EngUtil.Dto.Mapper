using System.Collections;
using System.Collections.Generic;

namespace engUtil.Dto
{
    public class MapConfig : IMapConfig
    {
        #region fields

        private readonly IMapper _mapper;

        #endregion

        #region ctor

        public MapConfig(IMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion

        #region methods

        /// <summary>
        /// Create new mapping 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <returns></returns>
        public Map<TSource, TTarget> CreateMappingFor<TSource,TTarget>()
        {
            var mapBuilder = new Map<TSource, TTarget>(_mapper);
            return mapBuilder;
        }

        /// <summary>
        /// Map instance to &lt;TTarget&gt; Type
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public TTarget MapTo<TTarget>(object instance)
        {
            return _mapper.MapTo<TTarget>(instance);
        }

        /// <summary>
        /// Map a Enumarable list of objects to IEnumerable&lt;TTarget&gt; Type
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="objectList">List of object</param>
        /// <returns></returns>
        public IEnumerable<TTarget> MapTo<TTarget>(IEnumerable objectList)
        {
            return _mapper.MapTo<TTarget>(objectList);
        }

        #endregion
    }
}
