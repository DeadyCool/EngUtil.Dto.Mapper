// --------------------------------------------------------------------------------
// <copyright filename="IExpressionMap.cs" date="12-13-2019">(c) 2019 All Rights Reserved</copyright>
// <author>Oliver Engels</author>
// --------------------------------------------------------------------------------
using System;
using System.Linq.Expressions;

namespace EngUtil.Dto
{
    public interface IDtoMap
    {
        Type SourceType { get; }

        Type TargetType { get; }

        string Description { get; set; }

        Expression Expression { get; }

        object MapObject(object instance);
    }
}
