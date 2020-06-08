// --------------------------------------------------------------------------------
// <copyright filename="MapDefinition.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EngUtil.Dto
{
    [MapDefinition]
    public abstract class DtoDefinition : IDtoDefinition
    {
        #region fields

        private static IList<IDtoMap> _mappingList;

        private IMapper _mapper = null;

        #endregion

        #region properties

        public IMapper Mapper
        {
            get { return _mapper; }
            set
            {
                if(_mapper == null)
                    _mapper = value;
                _mappingList = ((Mapper)_mapper).Mappings;
            }
        }

        #endregion

        #region methods

        public static TTarget MapTo<TTarget>(object instance)
        {
            return MappingHelper.MapTo<TTarget>(_mappingList, instance);
        }

        public static IEnumerable<TTarget> MapTo<TTarget>(IEnumerable instances)
        {
            return MappingHelper.MapTo<TTarget>(_mappingList, instances);
        }

        public static TTarget MapTo<TTarget,TSource>(TSource instance)
        {
            return MappingHelper.MapTo<TTarget>(_mappingList, instance);
        }

        public static IEnumerable<TTarget> MapTo<TTarget, TSource>(IEnumerable<TSource> instances)
        {
            return MappingHelper.MapTo<TTarget>(_mappingList, instances);
        }

        #endregion
    }
}
