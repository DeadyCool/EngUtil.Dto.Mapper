using System;
using System.Collections.Generic;
using System.Text;

namespace engUtil.SimpleMapper
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
    }
}
