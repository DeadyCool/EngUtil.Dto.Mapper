using System.Collections;
using System.Collections.Generic;

namespace engUtil.Dto
{
    public interface IMapConfig
    {
        Map<TSource, TTarget> CreateMappingFor<TSource, TTarget>();

        TTarget MapTo<TTarget>(object instance);

        IEnumerable<TTarget> MapTo<TTarget>(IEnumerable objectList);
    }
}
