namespace engUtil.SimpleMapper
{
    [MapDefinition]
    public class MapDefinition : IMapDefinition
    {
        private IMapper _mapper;
        
        public MapDefinition(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TTarget MapTo<TTarget>(object instance)
        {
            return _mapper.MapTo<TTarget>(instance);            
        }
    }
}
