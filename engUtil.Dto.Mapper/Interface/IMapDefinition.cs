using System.Collections;
using System.Collections.Generic;

namespace engUtil.Dto
{
    public interface IMapDefinition
    {
        TTarget MapTo<TTarget>(object instance);

        IEnumerable<TTarget> MapTo<TTarget>(IEnumerable instance);
    }
}
