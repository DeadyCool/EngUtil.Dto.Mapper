using System.Collections;
using System.Collections.Generic;

namespace engUtil.Dto
{
    public class MappingConfiguration
    {
        private IMapper _mapper;

        public MappingConfiguration(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Map<TSource, TTarget> CreateMappingFor<TSource,TTarget>()
        {
            var mapBuilder = new Map<TSource, TTarget>(_mapper);
            return mapBuilder;
        }        

        public TTarget MapTo<TTarget>(object instance)
        {
            return _mapper.MapTo<TTarget>(instance);
        }

        public IEnumerable<TTarget> MapTo<TTarget>(IEnumerable instance)
        {
            return _mapper.MapTo<TTarget>(instance);
        }
    }
}
