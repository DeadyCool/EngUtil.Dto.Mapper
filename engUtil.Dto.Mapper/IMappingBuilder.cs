// --------------------------------------------------------------------------------
// <copyright filename="IMappingConfig.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using EngUtil.Dto.Genercis;
using System.Collections;
using System.Collections.Generic;

namespace EngUtil.Dto
{
    public interface IMappingBuilder
    {
        IMappingConfig<TSource, TTarget> CreateMappingFor<TSource, TTarget>(string description = default)
            where TSource : class
            where TTarget : class;

        TTarget MapTo<TTarget>(object instance)
            where TTarget : class;

        IEnumerable<TTarget> MapTo<TTarget>(IEnumerable instances)
            where TTarget : class;
    }    
}
