using System;
using System.Collections;
using System.Collections.Generic;

namespace engUtil.Dto
{
    [MapDefinition]
    public class MapDefinition : IMapDefinition
    {
        private IMapper _mapper;

        public IMapper Mapper
        {
            get { return _mapper; }
            set
            {
                if(_mapper == null)
                    _mapper = value;
            }
        }

        public TTarget MapTo<TTarget>(object instance)
        {            
            return Mapper.MapTo<TTarget>(instance);            
        }

        public IEnumerable<TTarget> MapTo<TTarget>(IEnumerable instance)
        {
            return Mapper.MapTo<TTarget>(instance);
        }
    }
}
